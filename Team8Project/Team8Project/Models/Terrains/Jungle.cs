using System.Linq;
using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core;

namespace Team8Project.Models.Terrains
{
    public class Jungle : Terrain
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

        public override string HeroEffect(IHero hero)
        {
            switch (hero.HeroClass)
            {
                case HeroClass.Warrior:
                    hero.HealthPoints += 50;
                    return $"{hero.Name}'s healthpoints increased by 50";
                case HeroClass.Assasin:
                    foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.Damage))
                    {
                        ability.AbilityPower += 5;
                    }
                    return $"{hero.Name}'s damaging abilities power increased by 5";
                case HeroClass.Cleric:
                    foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.HOT))
                    {
                        ability.AbilityPower += 2;
                    }
                    return $"{hero.Name}'s HOT abilities increased by 2";
                case HeroClass.Mage:
                    hero.HealthPoints -= 25;
                    return $"{hero.Name}'s healthpoints decreased by 25";
                default:
                    return string.Empty;
            }
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
