using System.Linq;
using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core;

namespace Team8Project.Models.Terrains
{
    public class Jungle : Terrain, ITerrain
    {
        private static ITerrain instance;
        
        private Jungle() { }

        public static ITerrain Instance
        {
            get
            {
                if (instance == null) { instance = new Jungle(); }
                return instance;
            }
        }

        public override void HeroEffect(IHero hero)
        {
            switch (hero.HeroClass)
            {
                case HeroClass.Warrior:
                    hero.HealthPoints += 100;
                    GameEngine.Instance.Log.AppendLine(hero.Name + "'s healthpoints increased by 100");
                    break;
                case HeroClass.Assasin:
                    foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.Damage))
                    {
                        ability.AbilityPower += 5;
                    }
                    GameEngine.Instance.Log.AppendLine(hero.Name + "'s damaging abilities power increased by 5");
                    break;
                case HeroClass.Cleric:
                    foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.HOT))
                    {
                        ability.AbilityPower += 2;
                    }
                    GameEngine.Instance.Log.AppendLine(hero.Name + "'s HOT abilities increased by 2");
                    break;
                case HeroClass.Mage:
                    hero.HealthPoints -= 50;
                    GameEngine.Instance.Log.AppendLine(hero.Name + "'s healthpoints decreased by 50");
                    break;
            }
        }
        public override void ContinuousEffect(IHero hero)
        {
            if (this.IsDay == true)
            {
                foreach (var ability in hero.Abilities.Skip(3))
                {
                    ability.AbilityPower += 5;
                }
                GameEngine.Instance.Log.AppendLine(hero.Name + "'s damaging abilities power increased by 5");
            }
            else
            {
                foreach (var ability in hero.Abilities.Skip(3))
                {
                    ability.AbilityPower -= 2;
                }
                GameEngine.Instance.Log.AppendLine(hero.Name + "'s damaging abilities power decreased by 2");
            }
        }
    }
}
