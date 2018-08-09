using System;
using Team8Project.Common;
using Team8Project.Contracts;

namespace Team8Project.Core
{
    public class GameEngine
    {
        private static GameEngine instance;
        private readonly Factory factory;
        private TurnProcessor turn;
        private EffectManager effect;

        private GameEngine()
        {
            this.factory = Factory.Instance;
            this.turn = TurnProcessor.Instance;
            this.effect = EffectManager.Instance;
        }

        public void Run()
        {

            //Set Current Game Heroes

            Console.WriteLine($"Choose a character: 1.{HeroClass.Assasin}, 2.{HeroClass.Warrior}, 3.{HeroClass.Mage} 4.{HeroClass.Cleric}");
            var selectedHeroClass = HeroClass.NotSelectedYet;
            var input = Console.ReadLine();
            switch (input)
            {
                case "1": selectedHeroClass = HeroClass.Assasin; break;
                case "2": selectedHeroClass = HeroClass.Warrior; break;
                case "3": selectedHeroClass = HeroClass.Mage; break;
                case "4": selectedHeroClass = HeroClass.Cleric; break;
            }
            turn.FirstHero = factory.CreateHero(selectedHeroClass);

            input = Console.ReadLine();
            switch (input)
            {
                case "1": selectedHeroClass = HeroClass.Assasin; break;
                case "2": selectedHeroClass = HeroClass.Warrior; break;
                case "3": selectedHeroClass = HeroClass.Mage; break;
                case "4": selectedHeroClass = HeroClass.Cleric; break;
            }
            turn.SecondHero = factory.CreateHero(selectedHeroClass);

            turn.SetFirstTurnActiveHero();
            factory.CreateSpellBook(turn.FirstHero);
            factory.CreateSpellBook(turn.SecondHero);


            //START GAME
            while (true)
            {
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

                    //TODO FIX :

                    Console.WriteLine($"{turn.ActiveHero.Name}'s abilities: ");

                    int pos = 0;
                    foreach (var ability in turn.ActiveHero.Abilities)
                    {
                        pos++;
                        Console.WriteLine($"{pos}. {ability.Name}");
                    }

                    int inputAbility = int.Parse(Console.ReadLine());
                    var selectedAbility = turn.ActiveHero.Abilities[inputAbility - 1];


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
