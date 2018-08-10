using System;
using System.Collections.Generic;
using Team8Project.Common;
using Team8Project.Contracts;
using Team8Project.Core.Providers;
using Team8Project.IO;
using Team8Project.Models;
using Team8Project.Models.Terrains;

namespace Team8Project.Core
{
    public class GameEngine
    {
        private static GameEngine instance;
        private readonly Factory factory;
        private TurnProcessor turn;
        private EffectManager effect;
        private ITerrain terrain;
        private List<IHero> listHeros;

        private GameEngine()
        {
            this.factory = Factory.Instance;
            this.turn = TurnProcessor.Instance;
            this.effect = EffectManager.Instance;
            this.Reader = new ConsoleReader();
            this.Writer = new ConsoleWriter();
            this.listHeros = new List<IHero>();
        }

        public IReader Reader { get; set; }
        public IWriter Writer { get; set; }
        
        
        public void Run()
        {
            //Set Current Game Heroes

            Console.WriteLine($"Choose a character: 1.{HeroClass.Assasin}, 2.{HeroClass.Warrior}, 3.{HeroClass.Mage} 4.{HeroClass.Cleric}");
            string[] players = new string[2];

            players[0] = this.Reader.ConsoleReadLine();
            players[1] = this.Reader.ConsoleReadLine();
            this.ProcessCommand(players);
           
            turn.SetFirstTurnActiveHero();
            factory.CreateSpellBook(turn.FirstHero);
            factory.CreateSpellBook(turn.SecondHero);

            //choose terrain depending on user choice [0] [1] etc
            //if(userChoice == 0)
            //Get the only object available
            this.terrain = Jungle.getInstance();
            //apply effect
            terrain.HeroEffect(turn.ActiveHero);
            terrain.HeroEffect(turn.ActiveHero.Opponent);


            //START GAME
            while (true)
            {
                if (RandomProvider.Generate(1, 2) == 1)
                {
                    terrain.ContinuousEffect(turn.ActiveHero);
                    //TODO: needs message
                }
                else
                {
                    terrain.ContinuousEffect(turn.ActiveHero.Opponent);
                    //TODO: needs message
                }

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
                    ////refreshing cooldowns
                    foreach (IAbility ability in turn.ActiveHero.Abilities)
                    {
                        if (ability.OnCD == true)
                        {
                            ability.CD2++;
                            if (ability.CD2 == ability.Cd)
                            {
                                ability.OnCD = false;
                            }
                        }
                    }

                    Console.WriteLine($"{turn.ActiveHero.Name}'s abilities: ");

                    int pos = 0;
                    foreach (var ability in turn.ActiveHero.Abilities)
                    {
                        pos++;
                        Console.WriteLine($"{pos}. {ability.Name}");
                    }

                    string selectAbilityCommand = this.Reader.ConsoleReadKey();
                    var selectedAbility = this.ProcessCommand(selectAbilityCommand);
                    while (selectedAbility.OnCD == true)
                    {
                        Console.WriteLine("Chosen ability is on cooldown, choose another");
                        selectAbilityCommand = this.Reader.ConsoleReadKey();
                        selectedAbility = this.ProcessCommand(selectAbilityCommand);
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
                if (turn.TurnNumeber % 3 == 0)
                {
                    terrain.IsDay = (terrain.IsDay) ? false : true;
                    Console.WriteLine((terrain.IsDay) ? "Day has come" : "Night has come");
                }
            }
        }
        private void ProcessCommand(string[] players)
        {
            foreach (var player in players)
            {
                switch (player)
                {
                    case "1":
                        this.listHeros.Add(factory.CreateHero(HeroClass.Warrior));
                        break;
                    case "2":
                        this.listHeros.Add(factory.CreateHero(HeroClass.Mage));
                        break;
                    case "3":
                        this.listHeros.Add(factory.CreateHero(HeroClass.Assasin));
                        break;
                    case "4":
                        this.listHeros.Add(factory.CreateHero(HeroClass.Cleric));
                        break;
                    default:
                        throw new ArgumentException("I couldn't create you hero! :(");
                }
            }
            turn.FirstHero = this.listHeros[0];
            turn.SecondHero = this.listHeros[1];
        }
        private IAbility ProcessCommand(string key)
        {
            switch (key)
            {
                case "1":
                    return turn.ActiveHero.Abilities[0];
                case "2":
                    return turn.ActiveHero.Abilities[1];
                case "3":
                    return turn.ActiveHero.Abilities[2];
                default:
                    throw new ArgumentException("I couldn't return ability! :(");
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
