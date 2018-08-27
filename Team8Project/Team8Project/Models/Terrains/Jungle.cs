using System.Linq;
using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core;

namespace Team8Project.Models.Terrains
{
    public class Jungle : Terrain
    {
        public Jungle() { }

        public override void ApplyInitialAssasinEffect(IHero hero)
        {
            foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.Damage))
            {
                ability.AbilityPower += 5;
            }
            //return $"{hero.Name}'s damaging abilities power increased by 5";
        }

        public override void ApplyInitialClericEffect(IHero hero)
        {
            foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.HOT))
            {
                ability.AbilityPower += 2;
            }
            //return $"{hero.Name}'s HOT abilities increased by 2";
        }

        public override void ApplyInitialMageEffect(IHero hero)
        {
            hero.HealthPoints -= 25;
            //return $"{hero.Name}'s healthpoints decreased by 25";
        }

        public override void ApplyInitialWarriorEffect(IHero hero)
        {
            hero.HealthPoints += 50;
        }

        public override string ContinuousEffect(IHero hero)
        {
            if (this.IsDay == true)
            {
                foreach (var ability in hero.Abilities.Skip(3))
                {
                    ability.AbilityPower += 5;
                }
                return $"{hero.Name}'s damaging abilities power increased by 5";
            }
            else
            {
                foreach (var ability in hero.Abilities.Skip(3))
                {
                    ability.AbilityPower -= 2;
                }
                return $"{hero.Name}'s damaging abilities power decreased by 2";
            }
        }
    }
}
