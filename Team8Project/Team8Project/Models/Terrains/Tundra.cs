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
        private static ITerrain instance;

        //make the constructor private so that this class cannot be
        //instantiated
        private Tundra() { }

        public static ITerrain Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Tundra();
                }
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
                        ability.AbilityPower+= 20;
                    }
                    break;
                default:
                    break;
            }
        }
        public override void ContinuousEffect(IHero hero)
        {
            //if (!this.IsDay)
            //{
            //    foreach (var ef in hero.AppliedEffects.Where(x => x.Type == EffectType.Incapacitated))
            //    {
            //        ef.dur++;
            //    }
            //}
            //else
            //{
            //    hero.DmgEndOfRange -= 2;
            //}
        }
    }
}
