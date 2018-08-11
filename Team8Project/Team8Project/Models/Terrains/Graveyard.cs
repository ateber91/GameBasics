using System.Linq;
using System.Text;
using Team8Project.Common;
using Team8Project.Contracts;
using Team8Project.Core;

namespace Team8Project.Models.Terrains
{
    public class Graveyard: Terrain, ITerrain
    {
        //create an object of SingleObject
        private static ITerrain instance;

        //make the constructor private so that this class cannot be
        //instantiated
        private Graveyard() { }

        //Get the only object available
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
                        GameEngine.Instance.Log.AppendLine("DECREASED ALL OF WARRIORS DAMAGING ABILITY ATTACK POWER BY 2");
                    }
                    break;
                case HeroClass.Assasin:
                    var effects = hero.Abilities;

                    effects
                        .Where(e => e.Type == EffectType.DOT)
                        .ToList()
                        .ForEach(e => e.AbilityPower += 5);
                    GameEngine.Instance.Log.AppendLine("INCRESED ALL OF ASSASINS DOTS BY 5");
                    break;
                case HeroClass.Cleric:
                    var effects2 = hero.Abilities;

                    effects2
                        .Where(e => e.Type == EffectType.HOT)
                        .ToList()
                        .ForEach(e => e.AbilityPower-=5);
                    GameEngine.Instance.Log.AppendLine("DECREASED ALL OF CLERICS HOTS BY 5");
                    break;
                case HeroClass.Mage:
                    foreach (var ability in hero.Abilities.OfType<IEffect>())
                    {
                        ability.Cd++;
                        GameEngine.Instance.Log.AppendLine("INCREASED ALL OF MAGE'S EFFECT ABILITIES COOLDOWNS BY 1");
                    }
                    break;
                default:
                    break;
            }
        }
        public override void ContinuousEffect(IHero hero)
        {
            if (this.IsDay == true)
            {
                if (hero.AppliedEffects.Count != 0)
                {
                    var effects = hero.AppliedEffects;

                    effects
                        .Where(e => e.Type == EffectType.DOT)
                        .ToList()
                        .ForEach(e => e.CurrentStacks++);
                    GameEngine.Instance.Log.AppendLine("INCREASSED DURATION OF ALL APPLIED DOT EFFECTS");
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
                        .ForEach(e => e.CurrentStacks++);
                    GameEngine.Instance.Log.AppendLine("DECREASED DURATION OF ALL APPLIED HOT EFFECTS");
                }
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            //if (this.IsDay)
            //{
            //    sb.AppendLine("'s healthpoints increased by 1");
            //}
            //else
            //{
            //    sb.AppendLine("'s healthpoints reduced by 5");
            //}
            return sb.ToString();
        }
    }
}
