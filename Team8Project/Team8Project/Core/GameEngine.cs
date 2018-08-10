using System;
using System.Collections.Generic;
using System.Text;
using Team8Project.Common;
using Team8Project.Contracts;
using Team8Project.Core.Providers;
using Team8Project.IO;
using Team8Project.Models;
using Team8Project.Models.Terrains;

namespace Team8Project.Core
{
    public class GameEngine : IEngine
    {
        private const string INITIAL_MESSAGE = "Choose a character:\n 1.{0}\n 2.{1}\n 3.{2}\n 4.{3}";



        private static GameEngine instance;
        private readonly Factory factory;
        private TurnProcessor turn;
        private EffectManager effect;
        private TerrainManager terrainManager;
        private CommandProcessor commandProcessor;
        private List<IHero> listHeros;

        private GameEngine()
        {
            this.factory = Factory.Instance;
            this.turn = TurnProcessor.Instance;
            this.effect = EffectManager.Instance;
            this.Reader = new ConsoleReader();
            this.Writer = new ConsoleWriter();
            this.commandProcessor = new CommandProcessor();
            this.listHeros = new List<IHero>();
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
            players[0] = this.Reader.ConsoleReadLine();
            this.Writer.ConsoleWrite("Player 2: ");
            players[1] = this.Reader.ConsoleReadLine();
            this.Writer.ConsoleClear();
            //}
            this.listHeros = commandProcessor.ProcessCommand(players);
            turn.FirstHero = this.listHeros[0];
            turn.SecondHero = this.listHeros[1];

            turn.SetFirstTurnActiveHero();
            factory.CreateSpellBook(turn.FirstHero);
            factory.CreateSpellBook(turn.SecondHero);


            //START GAME
            while (true)
            {
                try
                {
                    this.Writer.PrintOnPosition(0, 60, "Initial terrain effects applied to both heroes");
                    this.Writer.PrintOnPosition(0, 150, $" Turn: {turn.TurnNumeber}", ConsoleColor.Red);
                    this.Writer.ConsoleWriteLine(new String('-', Console.WindowWidth));
                    for (int i = 1; i <= 2; i++)
                    {
                        Console.WriteLine($" Turn: {turn.TurnNumeber}. {turn.ActiveHero.HeroClass.ToString()} { turn.ActiveHero.Name} is active. HP: {turn.ActiveHero.HealthPoints}");

                        effect.AtTurnStart(turn.ActiveHero); //TODO: PRINT LOGIC FOR EFFECTS
                        if (turn.ActiveHero.AppliedEffects.Count == 0)
                        {
                            Console.WriteLine("Applied effects: No effects.");
                        }
                        else
                        {
                            Console.WriteLine($"Applied effects: {string.Join(", ", turn.ActiveHero.AppliedEffects)}");
                        }

                        Console.WriteLine($"{turn.ActiveHero.Name}'s abilities: ");

                        int pos = 0;
                        foreach (var ability in turn.ActiveHero.Abilities)
                        {
                            pos++;
                            Writer.ConsoleWriteLine($"{pos}. {ability.Print()}");
                        }

                        string selectAbilityCommand = this.Reader.ConsoleReadKey();
                        this.Writer.ConsoleWriteLine("");

                        var selectedAbility = commandProcessor.ProcessCommand(selectAbilityCommand);

                        while (selectedAbility.OnCD == true)
                        {
                            Console.WriteLine("Chosen ability is on cooldown, choose another");
                            selectAbilityCommand = this.Reader.ConsoleReadKey();
                            selectedAbility = commandProcessor.ProcessCommand(selectAbilityCommand);
                        }

                        turn.ActiveHero.UseAbility(selectedAbility);
                        Console.WriteLine($"{turn.ActiveHero.Name} uses {selectedAbility.Name} and {selectedAbility.ToString()}. {turn.ActiveHero.Opponent.Name} is left with {turn.ActiveHero.Opponent.HealthPoints} HP");

                        if (turn.ActiveHero.Opponent.HealthPoints < 0)
                        {
                            Console.WriteLine($"{turn.ActiveHero.Name.ToUpper()} WON! ");
                            break;
                        }
                        turn.EndTurn();
                    }
                    turn.NextTurn();
                    this.Writer.ConsoleClear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to continue!");
                    this.Reader.ConsoleReadKey();
                }
            }
        }

        public static GameEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameEngine();
                }

                return instance;
            }
        }
    }
}
