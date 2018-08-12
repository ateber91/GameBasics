using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core.Contracts;
using Team8Project.IO;
using Team8Project.IO.Contracts;

namespace Team8Project.Core
{
    public class GameEngine : IEngine
    {
        private const string INITIAL_MESSAGE = "Choose a character:\n 1.{0}\n 2.{1}\n 3.{2}\n 4.{3}";
        private const int LOG_ROW_POS = 12;
        private const int LOG_COL_POS = 0;

        private static GameEngine instance;
        private readonly Factory factory;
        private TurnProcessor turn;
        private EffectManager effect;
        private TerrainManager terrainManager;
        private CommandProcessor commandProcessor;
        private List<IHero> listHeros;
        private StringBuilder log;
        private bool endGame = false;

        private GameEngine()
        {
            this.factory = Factory.Instance;
            this.turn = TurnProcessor.Instance;
            this.effect = EffectManager.Instance;
            this.Reader = new ConsoleReader();
            this.Writer = new ConsoleWriter();
            this.commandProcessor = new CommandProcessor();
            this.listHeros = new List<IHero>();
            this.log = new StringBuilder();
            this.terrainManager = TerrainManager.Instance;
        }

        public IReader Reader { get; set; }
        public IWriter Writer { get; set; }


        public void Run()
        {
            Console.SetWindowSize(160, 40);

            this.Writer.ConsoleWriteLine(string.Format(INITIAL_MESSAGE, HeroClass.Assasin, HeroClass.Warrior, HeroClass.Mage, HeroClass.Cleric));
            this.Writer.ConsoleWriteLine(new String('-', Console.WindowWidth));
            string[] players = new string[2];

            //while (true)
            //{

            this.Writer.ConsoleWrite("Player 1: ");
            players[0] = this.Reader.ConsoleReadKey();
            this.Writer.ConsoleWriteLine("");
            this.Writer.ConsoleWrite("Player 2: ");
            players[1] = this.Reader.ConsoleReadKey();
            this.Writer.ConsoleClear();
            //}
            this.listHeros = commandProcessor.ProcessCommand(players);
            turn.FirstHero = this.listHeros[0];
            turn.SecondHero = this.listHeros[1];

            turn.SetFirstTurnActiveHero();
            factory.CreateSpellBook(turn.FirstHero);
            factory.CreateSpellBook(turn.SecondHero);

            this.terrainManager.SetTerrain();
            this.terrainManager.ApplyInitialEffects(turn.ActiveHero);


            //START GAME
            while (true)
            {
                try
                {
                    this.printHeader();

                    this.terrainManager.ApplyContinuousEffect(this.turn.ActiveHero, this.turn.ActiveHero.Opponent);
                    //TODO: FIX printing!
                    this.Writer.ConsoleClear();
                    this.Writer.PrintOnPosition(LOG_ROW_POS - 1, LOG_COL_POS, new String('-', Console.WindowWidth));
                    this.Writer.PrintOnPosition(LOG_ROW_POS, LOG_COL_POS, log.ToString());
                    this.printHeader();

                    Act(turn.ActiveHero); //first hero move
                    turn.EndAct();
                    this.Writer.ConsoleClear();
                    this.Writer.PrintOnPosition(LOG_ROW_POS - 1, LOG_COL_POS, new String('-', Console.WindowWidth));
                    this.Writer.PrintOnPosition(LOG_ROW_POS, LOG_COL_POS, log.ToString());
                    this.printHeader();

                    if (this.endGame)
                    {
                        return;
                    }
                    Act(turn.ActiveHero); //second hero move
                    turn.EndAct();
                    this.Writer.PrintOnPosition(LOG_ROW_POS - 1, LOG_COL_POS, new String('-', Console.WindowWidth));
                    this.Writer.PrintOnPosition(LOG_ROW_POS, LOG_COL_POS, log.ToString());
                    turn.UpdateCooldowns(turn.ActiveHero);

                    turn.NextTurn();
                    if (turn.TurnNumber % 3 == 0)
                    {
                        this.terrainManager.ChangeDayNight();
                    }

                    if (this.endGame) { return; }
                    Writer.ConsoleWriteLine("******************************************************************");

                    this.Writer.ConsoleClear();
                    this.Writer.PrintOnPosition(LOG_ROW_POS - 1, LOG_COL_POS, new String('-', Console.WindowWidth));
                    this.Writer.PrintOnPosition(LOG_ROW_POS, LOG_COL_POS, log.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to continue!");
                    this.Reader.ConsoleReadKey();
                }
            }
        }

        private void Act(IHero activeHero)
        {
            //checks for status incapacitated
            if ((turn.ActiveHero.AppliedEffects.Any(x => x.Type == EffectType.Incapacitated)))
            {
                log.AppendLine($"{turn.ActiveHero.HeroClass} {turn.ActiveHero.Name} is incapacitated!. Cannot act!");

                turn.ActiveHero.AppliedEffects.Remove(turn.ActiveHero.AppliedEffects.First(x => x.Type == EffectType.Incapacitated));
            }
            else
            {
                effect.AtTurnStart(turn.ActiveHero); // checks for statuses : DOT,HOT
                this.Writer.ConsoleClear();
                this.Writer.PrintOnPosition(LOG_ROW_POS - 1, LOG_COL_POS, new String('-', Console.WindowWidth));
                this.Writer.PrintOnPosition(LOG_ROW_POS, LOG_COL_POS, log.ToString());
                this.printHeader();
                effect.RemoveExpired(activeHero);

                Writer.ConsoleWriteLine($"{turn.ActiveHero.HeroClass.ToString()} { turn.ActiveHero.Name} is active. HP: {turn.ActiveHero.HealthPoints}");

                this.Writer.ConsoleWriteLine($"{turn.ActiveHero.Name}'s abilities: ");

                int pos = 0;
                foreach (var ability in turn.ActiveHero.Abilities)
                {
                    pos++;
                    Writer.ConsoleWriteLine($"{pos}. {ability.Print()}");
                }


                if (turn.ActiveHero.AppliedEffects.Count == 0) { this.Writer.ConsoleWriteLine("Applied effects: No effects."); }
                else { this.Writer.ConsoleWriteLine($"Applied effects: {string.Join(", ", turn.ActiveHero.AppliedEffects)}"); }

                //string selectAbilityCommand = this.Reader.ConsoleReadKey();
                //this.Writer.ConsoleWriteLine("");
                //var selectedAbility = commandProcessor.ProcessCommand(selectAbilityCommand);


                string selectAbilityCommand = this.Reader.ConsoleReadKey();
                var selectedAbility = this.commandProcessor.ProcessCommand(selectAbilityCommand);

                if(selectedAbility.OnCD == true)
                {
                    log.AppendLine("Chosen ability is on cooldown, choose another");
                    this.Writer.PrintOnPosition(LOG_ROW_POS, LOG_COL_POS, log.ToString());
                    Console.SetCursorPosition(2, 9);
                    while (selectedAbility.OnCD == true)
                    {
                        selectAbilityCommand = this.Reader.ConsoleReadKey();
                        this.Writer.ConsoleClear();
                        this.Writer.ConsoleWriteLine("I told you to choose other option!!! Try again. I will be wathcing you!");
                        selectedAbility = this.commandProcessor.ProcessCommand(selectAbilityCommand);
                    }
                }

                turn.ActiveHero.UseAbility(selectedAbility);

                this.Log.AppendLine($"{turn.ActiveHero.Name} uses {selectedAbility.Name} and {selectedAbility.ToString()}.");
            }
            if (turn.ActiveHero.Opponent.HealthPoints < 0)
            {
                this.Writer.ConsoleClear();
                Console.Beep();
                this.Writer.PrintOnPosition(0, 0, $"{turn.ActiveHero.Name.ToUpper()} WON!", ConsoleColor.Green);
                Thread.Sleep(5000);
                Console.Beep();
                this.endGame = true;
            }
        }

        private void printHeader()
        {
            this.Writer.PrintOnPosition(0, 0, this.terrainManager.TarrainType);
            this.Writer.PrintOnPosition(0, 60, "Initial terrain effects applied to both heroes");
            this.Writer.PrintOnPosition(0, 150, $" Turn: {turn.TurnNumber}", ConsoleColor.Red);
            this.Writer.ConsoleWriteLine(new String('-', Console.WindowWidth));
        }

        public static GameEngine Instance
        {
            get
            {
                if (instance == null) { instance = new GameEngine(); }
                return instance;
            }
        }

        public StringBuilder Log
        {
            get
            {
                return this.log;
            }
            set
            {
                this.log = value;
            }
        }
    }
}