using System;
using Team8Project.Common;
using Team8Project.Contracts;
using Team8Project.Core.Providers;
using Team8Project.Models;
using Team8Project.Models.Terrains;

namespace Team8Project.Core
{
    public class GameEngine
    {
        private static GameEngine instance;
        private readonly Factory factory;
        private TurnProcessor turn;
        private ITerrain terrain;

        private GameEngine()
        {
            this.factory = Factory.Instance;
            this.turn = TurnProcessor.Instance;
        }

        public void Run()
        {
            //Set Current Game Heroes
            var selectedHeroClassForPlayerOne = HeroClass.Warrior; //set class depending on choice
            var selectedHeroClassForPlayerTwo = HeroClass.Assasin;

            turn.FirstHero = factory.CreateHero(selectedHeroClassForPlayerOne);
            turn.SecondHero = factory.CreateHero(selectedHeroClassForPlayerTwo);

            turn.SetFirstTurnActiveHero();

            //choose terrain depending on user choice [0] [1] etc
            //if(userChoice == 0)
            //Get the only object available
            this.terrain = Jungle.getInstance();
            this.terrain = Graveyard.getInstance();
            //apply effect
            terrain.HeroEffect(turn.ActiveHero);
            terrain.HeroEffect(turn.ActiveHero.Opponent);


            //START GAME
            while (true)
            {

                Console.WriteLine($" Turn: {turn.TurnNumeber}. {turn.ActiveHero.HeroClass.ToString()} { turn.ActiveHero.Name} is active. HP: {turn.ActiveHero.HealthPoints}");

                if (RandomProvider.Generate(1, 2) == 1)
                {
                    terrain.ContinuousEffect(turn.ActiveHero);
                }
                else
                {
                    terrain.ContinuousEffect(turn.ActiveHero.Opponent);
                }

                //cast depending of selection [0],[1],[2] 
                var selectedAbility = turn.ActiveHero.Abilities[0];

                turn.ActiveHero.UseAbility(selectedAbility);
                Console.WriteLine($"{turn.ActiveHero.Name} uses {selectedAbility.Name} and {selectedAbility.ToString()}. {turn.ActiveHero.Opponent.Name} is left with {turn.ActiveHero.Opponent.HealthPoints} HP");

                if (turn.ActiveHero.Opponent.HealthPoints < 0)
                {
                    Console.WriteLine($"{turn.ActiveHero.Name.ToUpper()} WON! ");
                    break;
                }
                turn.NextTurn();
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
