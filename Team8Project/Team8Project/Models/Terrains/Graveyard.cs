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
                    hero.HealthPoints -= 50;
                    break;
                case HeroClass.Assasin:
                    hero.DmgStartOfRange += 5;
                    hero.DmgEndOfRange += 5;
                    break;
                case HeroClass.Cleric:
                    hero.HealthPoints += 50;
                    break;
                case HeroClass.Mage:
                    hero.DmgStartOfRange -= 5;
                    hero.DmgEndOfRange -= 5;
                    break;
                default:
                    break;
            }
        }
        public override void ContinuousEffect(IHero hero)
        {
            if (hero.HealthPoints > 30)
            {
                hero.HealthPoints -= 10;
            }
        }
    }
}
