using System.Linq;
using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core;

namespace Team8Project.Models.Terrains
{
    public class Tundra : Terrain
    {

        public Tundra() { }

        public override void ApplyInitialAssasinEffect(IHero hero)
        {
            foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.Damage))
            {
                ability.AbilityPower -= 2;
            }
            //return $"{hero.Name}'s damaging abilities decreased by 2";
        }

        public override void ApplyInitialClericEffect(IHero hero)
        {
            foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.Damage))
            {
                ability.AbilityPower -= 2;
            }
            //return $"{hero.Name}'s damaging abilities decreased by 2";
        }

        public override void ApplyInitialMageEffect(IHero hero)
        {
            foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.Damage))
            {
                ability.AbilityPower += 5;
            }
            //return $"{hero.Name}'s damaging abilities power increased by 5";
        }

        public override void ApplyInitialWarriorEffect(IHero hero)
        {
            hero.HealthPoints -= 25;
            //return $"{hero.Name}'s health points decreased by 25";
        }

        public override string ContinuousEffect(IHero hero)
        {
            if (this.IsDay == false)
            {
                foreach (var ability in hero.Abilities.Skip(1))
                {
                    if (ability.OnCD == false)
                    {
                        ability.OnCD = true;
                        ability.CDCounter = ability.Cd - 1;
                    }
                }
             return $"{hero.Name}'s available abilities are on cool down";
            }
            else
            {
                if (hero.AppliedEffects.Count != 0)
                {
                    var stuns = hero.AppliedEffects.FirstOrDefault(e => e.Type == EffectType.Incapacitated);
                    if (stuns != null)
                    {
                        hero.AppliedEffects[hero.AppliedEffects.IndexOf(stuns)].CurrentStacks--;
                        return $"{hero.Name}'s duration of all applied incapacitating effects increased by 1";
                    }
                    return string.Empty;
                }
                return string.Empty;
            }
        }
    }
}
