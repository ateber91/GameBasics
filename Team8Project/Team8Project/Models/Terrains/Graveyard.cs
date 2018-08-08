using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Common;
using Team8Project.Contracts;

namespace Team8Project.Models.Terrains
{
    public class Graveyard: Terrain, ITerrain
    {
        //create an object of SingleObject
        private static ITerrain instance = new Graveyard();

        //make the constructor private so that this class cannot be
        //instantiated
        private Graveyard() { }

        //Get the only object available
        public static ITerrain getInstance()
        {
            return instance;
        }

        public static ITerrain Instance
        {
            get
            {
                return instance;
            }
        }

        public override void HeroEffect(IHero hero)
        {
            switch (hero.HeroClass)
            {
                case HeroClass.Warrior:
                    hero.HealthPoints += 100;
                    break;
                case HeroClass.Assasin:
                    hero.DmgStartOfRange -= 10;
                    hero.DmgEndOfRange += 10;
                    break;
                case HeroClass.Cleric:
                    hero.HealthPoints -= 100;
                    break;
                case HeroClass.Mage:
                    hero.DmgStartOfRange += 10;
                    hero.DmgEndOfRange -= 10;
                    break;
                default:
                    break;
            }
        }
    }
}
