using System.Linq;
using System.Text;
using Team8Project.Common;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core;

namespace Team8Project.Models.Terrains
{
    public class Graveyard : Terrain
    {
        public Graveyard() { }

        public override void ApplyInitialEffect(IHero hero)
        {
            hero.InitializeGraveyard();
        }

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
    }
}
