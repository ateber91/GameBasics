using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Models.Characters
{
    public class Cleric : Hero, IHero
    {
        public Cleric(string name, HeroClass heroClass, int healthPoints, int dmgStartOfRange, int dmgEndOfRange) : base(name, heroClass, healthPoints, dmgStartOfRange, dmgEndOfRange)
        {
        }
        public override void InitializeGraveyard()
        {
            foreach (var ability in this.Abilities.Where(x => x.Type == EffectType.HOT))
            {
                ability.AbilityPower -= 5;
            }
        }
        public override void InitializeJungle()
        {
            foreach (var ability in this.Abilities.Where(x => x.Type == EffectType.HOT))
            {
                ability.AbilityPower += 2;
            }
            //return $"{hero.Name}'s HOT abilities increased by 2";
        }
        public override void InitializeTundra()
        {
            foreach (var ability in this.Abilities.Where(x => x.Type == EffectType.Damage))
            {
                ability.AbilityPower -= 2;
            }
            //return $"{hero.Name}'s damaging abilities decreased by 2";
        }
    }
}