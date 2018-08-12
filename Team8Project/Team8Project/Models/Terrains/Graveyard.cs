using System.Linq;
using System.Text;
using Team8Project.Common;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core;

namespace Team8Project.Models.Terrains
{
    public class Graveyard : Terrain, ITerrain
    {
        private static ITerrain instance;

        private Graveyard() { }

        public static ITerrain Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Graveyard();
                }
                return instance;
            }
        }

        public override void HeroEffect(IHero hero)
        {
            switch (hero.HeroClass)
            {
                case HeroClass.Warrior:
                    foreach (var ability in hero.Abilities.OfType<IDamagingAbility>())
                    {
                        ability.AbilityPower -= 10;
                    }
                    GameEngine.Instance.Log.AppendLine(hero.Name + "'s damaging abilities decreased by 10");
                    break;
                case HeroClass.Assasin:
                    foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.DOT))
                    {
                        ability.AbilityPower += 5;
                    }
                    GameEngine.Instance.Log.AppendLine(hero.Name + "'s DOT abilities power increased by 5");
                    break;
                case HeroClass.Cleric:
                    foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.HOT))
                    {
                        ability.AbilityPower -= 5;
                    }
                    GameEngine.Instance.Log.AppendLine(hero.Name + "'s HOT abilities power decreased by 5");
                    break;
                case HeroClass.Mage:
                    foreach (var ability in hero.Abilities.OfType<IEffect>())
                    {
                        ability.Cd++;
                    }
                    GameEngine.Instance.Log.AppendLine(hero.Name + "'s cooldowns on effect abilities increased by 1");
                    break;
                default:
                    break;
            }
        }
        public override void ContinuousEffect(IHero hero)
        {
            if (hero.AppliedEffects.Count != 0)
            {
                if (this.IsDay == false)
                {
                    var dot = hero.AppliedEffects.FirstOrDefault(e => e.Type == EffectType.DOT);
                    if (dot != null)
                    {
                        hero.AppliedEffects[hero.AppliedEffects.IndexOf(dot)].CurrentStacks--;
                        GameEngine.Instance.Log.AppendLine(hero.Name + "'s duration of all applied DOT effects increased by 1");
                    }
                }
                else
                {
                    var hot = hero.AppliedEffects.FirstOrDefault(e => e.Type == EffectType.HOT);
                    if (hot != null)
                    {
                        hero.AppliedEffects[hero.AppliedEffects.IndexOf(hot)].CurrentStacks++;
                        GameEngine.Instance.Log.AppendLine(hero.Name + "'s duration of all applied HOT effects decreased by 1");
                    }
                }
            }
        }
    }
}
