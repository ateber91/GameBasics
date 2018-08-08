using System;
using Team8Project.Common;

namespace Team8Project.Core
{
    public class GameEngine
    {
        private static GameEngine instance; 
        private readonly Factory factory;
        private TurnProcessor turn;

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
