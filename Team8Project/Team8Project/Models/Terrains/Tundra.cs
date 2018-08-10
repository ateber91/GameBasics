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
                    hero.DmgStartOfRange += 10;
                    hero.DmgEndOfRange += 10;
                    break;
                case HeroClass.Cleric:
                    hero.HealthPoints -= 100;
                    break;
                case HeroClass.Mage:
                    hero.HealthPoints += 50;
                    //foreach(IDamagingAbility ability in hero.Abilities)
                    //{
                    //    ability.AbilityPower+= 20;
                    //}
                    break;
                default:
                    break;
            }
        }
        public override void ContinuousEffect(IHero hero)
        {
            if (!this.IsDay)
            {
                var effects = hero.AppliedEffects;

                effects
                    .Where(e => e.Type == EffectType.Incapacitated)
                    .ToList()
                    .ForEach(e => e.Duration++);
            }
            else
            {
                hero.DmgEndOfRange -= 2;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (!this.IsDay)
            {
                sb.AppendLine("Incapacitating effects extended with 1 turn");
            }
            else
            {
                sb.AppendLine("Hero max damage reduced by 2");
            }
            return sb.ToString();
        }
    }
}
