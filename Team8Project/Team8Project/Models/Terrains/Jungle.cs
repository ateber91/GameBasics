using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Models.Terrains
{
    public class Jungle : Terrain, ITerrain
    {
        //create an object of SingleObject
        private static ITerrain instance;

        //make the constructor private so that this class cannot be
        //instantiated
        private Jungle() { }

        public static ITerrain Instance
        {
            get
            {
                if (instance == null) { instance = new Jungle(); }
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
                    hero.DmgEndOfRange -= 10;
                    break;
                case HeroClass.Cleric:
                    hero.HealthPoints -= 100;
                    break;
                case HeroClass.Mage:
                    hero.DmgStartOfRange += 10;
                    hero.DmgEndOfRange += 10;
                    break;
            }
        }
        public override void ContinuousEffect(IHero hero)
        {
            if (this.IsDay == true)
            {
                hero.HealthPoints += 10;
            }
            else
            {
                hero.HealthPoints -= 2;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (this.IsDay)
            {
                sb.AppendLine("'s healthpoints increased by 10");
            }
            else
            {
                sb.AppendLine("'s healthpoints reduced by 2");
            }
            return sb.ToString();
        }
    }
}
