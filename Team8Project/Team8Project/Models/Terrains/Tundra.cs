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
        
        public override void ApplyInitialEffect(IHero hero)
        {
            hero.InitializeTundra();
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
