using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Common;
using Team8Project.Contracts;

namespace Team8Project.Models.Terrains
{
    public class Tundra:Terrain
    {
        //create an object of SingleObject
        private static ITerrain instance = new Tundra();

        //make the constructor private so that this class cannot be
        //instantiated
        private Tundra() { }

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
                    hero.DmgStartOfRange += 10;
                    hero.DmgEndOfRange += 10;
                    break;
                case HeroClass.Assasin:
                    hero.DmgStartOfRange -= 10;
                    hero.DmgEndOfRange -= 10;
                    break;
                case HeroClass.Cleric:
                    hero.HealthPoints -= 100;
                    break;
                case HeroClass.Mage:
                    foreach(IDamagingAbility ability in hero.Abilities)
                    {
                        ability.Cd--;
                    }
                    hero.HealthPoints += 100;
                    break;
                default:
                    break;
            }
        }
        public override void ContinuousEffect(IHero hero)
        {
            if (!this.IsDay)
            {
                var result =
            (from effect in hero.AppliedEffects
            where effect.Type == EffectType.Incapacitated
            select effect).ToList();
                result.ForEach(x => x.Duration++);
            }
            else
            {
                hero.DmgEndOfRange -= 2;
            }
        }
    }
}
