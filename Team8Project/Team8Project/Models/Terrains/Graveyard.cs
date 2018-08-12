using System.Linq;
using System.Text;
using Team8Project.Common;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core;

namespace Team8Project.Models.Terrains
{
    public class Graveyard: Terrain, ITerrain
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
                    var effects = hero.Abilities;

                    effects
                        .Where(e => e.Type == EffectType.DOT)
                        .ToList()
                        .ForEach(e => e.AbilityPower += 5);
                    GameEngine.Instance.Log.AppendLine(hero.Name + "'s DOT abilities power increased by 5");
                    break;
                case HeroClass.Cleric:
                    var effects2 = hero.Abilities;

                    effects2
                        .Where(e => e.Type == EffectType.HOT)
                        .ToList()
                        .ForEach(e => e.AbilityPower-=5);
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
            if (!this.IsDay == true)
            {
                if (hero.AppliedEffects.Count != 0)
                {
                    var effects = hero.AppliedEffects;

                    effects
                        .Where(e => e.Type == EffectType.DOT)
                        .ToList()
                        .ForEach(e => e.CurrentStacks++);
                    GameEngine.Instance.Log.AppendLine(hero.Name + "'s duration of all applied DOT effects increased by 1");
                }
                else
                {
                    GameEngine.Instance.Log.AppendLine("No applied statuses to be affected");
                }
            }
            else
            {
                if (hero.AppliedEffects.Count != 0)
                {
                    var effects = hero.AppliedEffects;

                    effects
                        .Where(e => e.Type == EffectType.HOT)
                        .ToList()
                        .ForEach(e => e.CurrentStacks--);
                    GameEngine.Instance.Log.AppendLine(hero.Name + "'s duration of all applied HOT effects decreased by 1");
                }
                else
                {
                    GameEngine.Instance.Log.AppendLine("No applied statuses to be affected");
                }
            }
        }
    }
}
