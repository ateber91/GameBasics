using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Models.Characters
{
    public class Mage : Hero, IHero
    {
        public Mage(string name, HeroClass heroClass, int healthPoints, int dmgStartOfRange, int dmgEndOfRange) : base(name, heroClass, healthPoints, dmgStartOfRange, dmgEndOfRange)
        {
        }
        public override void InitializeGraveyard()
        {
            foreach (var ability in this.Abilities.OfType<IEffect>())
            {
                ability.Cd++;
            }
            //return $"{hero.Name}'s cooldowns on effect abilities increased by 1";
        }
        public override void InitializeJungle()
        {
            this.HealthPoints -= 25;
            //return $"{hero.Name}'s healthpoints decreased by 25";
        }
        public override void InitializeTundra()
        {
            foreach (var ability in this.Abilities.Where(x => x.Type == EffectType.Damage))
            {
                ability.AbilityPower += 5;
            }
            //return $"{hero.Name}'s damaging abilities power increased by 5";
        }
    }
}