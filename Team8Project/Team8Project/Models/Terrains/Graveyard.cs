using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Models.Terrains
{
    public class Graveyard: Terrain, ITerrain
    {
        //create an object of SingleObject
        private static ITerrain instance;

        //make the constructor private so that this class cannot be
        //instantiated
        private Graveyard() { }

        //Get the only object available
        public static ITerrain Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Graveyard();
                }
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
            if (hero.HealthPoints > 20)
            {
                if (this.IsDay == true)
                {
                    hero.HealthPoints -= 1;
                }
                else
                {
                    hero.HealthPoints -= 5;
                }
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (this.IsDay)
            {
                sb.AppendLine("'s healthpoints increased by 1");
            }
            else
            {
                sb.AppendLine("'s healthpoints reduced by 5");
            }
            return sb.ToString();
        }
    }
}
