using System;
using System.Collections.Generic;
using System.Linq;
using Team8Project.Common;
using Team8Project.Contracts;
using Team8Project.Core.Providers;
using Team8Project.Models;
using Team8Project.Models.Magic;

namespace Team8Project.Core
{
    public class GameEngine
    {

        private static GameEngine instance;

        //  private readonly ICommandParser parser;
        // private readonly ICommandProcessor commandProcessor;
        private readonly Factory factory;
        private Turn turn;



        private GameEngine()
        {
            //    this.parser = CommandParser.Instance;
            //    this.commandProcessor = CommandProcessor.Instance;
            this.factory = Factory.Instance;
            this.turn = Turn.Instance;
        }

        public void Run()
        {

            //Set Current Game Heroes
            var seceltedHeroClassForPlayerOne = HeroClass.Warrior; //set class depending on choice
            var seceltedHeroClassForPlayerTwo = HeroClass.Assasin;

            turn.ActiveHero = factory.CreateHero(seceltedHeroClassForPlayerOne);
            turn.ActiveHero.Opponent = factory.CreateHero(seceltedHeroClassForPlayerTwo);

            //set heroes realtionship 
            turn.ActiveHero.Opponent.Opponent = turn.ActiveHero;

            turn.ActiveHero = turn.SetWhoIsFirst();

            //START GAME
            while (true)
            {
                Console.WriteLine($" Turn: {turn.TurnNumeber}. {turn.ActiveHero.HeroClass.ToString()} { turn.ActiveHero.Name} is active. HP: {turn.ActiveHero.HealthPoints}");

                //cast depending of selection [0],[1],[2] 
                var selectedAbility = turn.ActiveHero.Abilities[0];

                turn.ActiveHero.UseAbility(selectedAbility);
                Console.WriteLine($"{turn.ActiveHero.Name} uses {selectedAbility.Name} and {selectedAbility.ToString()}. {turn.ActiveHero.Opponent.Name} is left with {turn.ActiveHero.Opponent.HealthPoints} HP");

                if (turn.ActiveHero.Opponent.HealthPoints < 0)
                {
                    Console.WriteLine($"{turn.ActiveHero.Name.ToUpper()} WON! ");
                    break;
                }
                turn.ActiveHero = turn.SwapTurnHolder();
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
