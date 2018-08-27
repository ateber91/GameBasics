using System.Linq;
using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Models.Terrains
{
    public class Graveyard : Terrain
    {
        public Graveyard() { }

        public override string ContinuousEffect(IHero hero)
        {
            if (hero.AppliedEffects.Count != 0)
            {
                if (this.IsDay == false)
                {
                    var dot = hero.AppliedEffects.FirstOrDefault(e => e.Type == EffectType.DOT);
                    if (dot != null)
                    {
                        hero.AppliedEffects[hero.AppliedEffects.IndexOf(dot)].CurrentStacks++;                       
                        return $"{hero.Name}'s duration of all applied DOT effects increased by 1";
                    }
                    return string.Empty;
                }
                else
                {
                    var hot = hero.AppliedEffects.FirstOrDefault(e => e.Type == EffectType.HOT);
                    if (hot != null)
                    {
                        hero.AppliedEffects[hero.AppliedEffects.IndexOf(hot)].CurrentStacks--;
                        return $"{hero.Name}'s duration of all applied HOT effects decreased by 1";
                    }
                    return string.Empty;
                }
            }
            return string.Empty;
        }
        
        public override void ApplyInitialWarriorEffect(IHero hero)
        {
            foreach (var ability in hero.Abilities.OfType<IDamagingAbility>())
            {
                ability.AbilityPower -= 10;
            }
            //return $"{hero.Name}'s damaging abilities decreased by 10";
        }

        public override void ApplyInitialAssasinEffect(IHero hero)
        {
            foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.DOT))
            {
                ability.AbilityPower += 5;
            }
            //return $"{hero.Name}'s DOT abilities power increased by 5";
        }

        public override void ApplyInitialClericEffect(IHero hero)
        {
            foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.HOT))
            {
                ability.AbilityPower -= 5;
            }
        }

        public override void ApplyInitialMageEffect(IHero hero)
        {
            foreach (var ability in hero.Abilities.OfType<IEffect>())
            {
                ability.Cd++;
            }
            //return $"{hero.Name}'s cooldowns on effect abilities increased by 1";
        }
    }
}
