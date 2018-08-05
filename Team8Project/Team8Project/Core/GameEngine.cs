using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Common;
using Team8Project.Contracts;
using Team8Project.Core;
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
        private readonly HeroPool heroPool;
        //spell pool TODO
        private readonly IList<IHero> currentGameHeroes;
        private IHero activeHero;
        private int turn;



        private GameEngine()
        {
            //    this.parser = CommandParser.Instance;
            //    this.commandProcessor = CommandProcessor.Instance;
            this.factory = Factory.Instance;
            this.heroPool = HeroPool.Instance;
            this.currentGameHeroes = new List<IHero>();
        }

        public void Run()
        {

            //Set Current Game Heroes
            var seceltedHeroClassForPlayerOne = HeroClass.Warrior; //set class depending on choice
            var seceltedHeroClassForPlayerTwo = HeroClass.Assasin;
            currentGameHeroes.Add(heroPool.Heroes.FirstOrDefault(x => x.HeroClass == seceltedHeroClassForPlayerOne));
            currentGameHeroes.Add(heroPool.Heroes.FirstOrDefault(x => x.HeroClass == seceltedHeroClassForPlayerTwo));

            //todo: set spells from spellPool
            //create spells
            DamagingAbility fireball = new DamagingAbility("fireball", 2, 50);
            DamagingAbility sinisterStrike = new DamagingAbility("sinister strike", 2, 40);
            //add spell and set spell owner
            currentGameHeroes.First().AddAbility(fireball);
            fireball.Caster = currentGameHeroes.First();
            currentGameHeroes.Last().AddAbility(sinisterStrike);
            sinisterStrike.Caster = currentGameHeroes.Last();

            //set heroes realtionship 
            currentGameHeroes.First().Opponent = currentGameHeroes.Last();
            currentGameHeroes.Last().Opponent = currentGameHeroes.First();

            activeHero = SetWhoIsFirst(currentGameHeroes.First());

            //START GAME
            while (true)
            {
                Console.WriteLine($" Turn: {turn++}. {activeHero.Name} is active");

                //cast
                activeHero.UseAbility(activeHero.Abilities[0]);
                Console.WriteLine($"{activeHero.Name} uses {activeHero.Abilities[0].Name} and {activeHero.Abilities[0].ToString()}. {activeHero.Opponent.Name} is left with {activeHero.Opponent.HealthPoints} HP");

                if (activeHero.Opponent.HealthPoints < 0)
                {
                    Console.WriteLine($"{activeHero.Name.ToUpper()} WON! ");
                    break;
                }
                activeHero = SwapTurnHolder(activeHero);
            }

        }

        private static IHero SwapTurnHolder(IHero active)
        {
            active.HasTurn = false;
            active.Opponent.HasTurn = true;
            return active.Opponent;
        }

        public static IHero SetWhoIsFirst(IHero playerOne)
        {

            var res = RandomProvider.Generate(1, 3);

            if (res == 1)
            {
                playerOne.HasTurn = true;
                playerOne.Opponent.HasTurn = false;
                return playerOne;
            }
            else
            {
                playerOne.Opponent.HasTurn = true;
                playerOne.HasTurn = false;
                return playerOne.Opponent;
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
