using System.Linq;
using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core;

namespace Team8Project.Models.Terrains
{
    public class Tundra : Terrain
    {
        //create an object of SingleObject
        private static ITerrain instance;

        //make the constructor private so that this class cannot be
        //instantiated
        private Tundra() { }

        public static ITerrain Instance
        {
            get
            {
                if (instance == null) { instance = new Tundra(); }
                return instance;
            }
        }

        public override string HeroEffect(IHero hero)
        {
            switch (hero.HeroClass)
            {
                case HeroClass.Warrior:
                    hero.HealthPoints -= 25;
                    return $"{hero.Name}'s health points decreased by 25";
                case HeroClass.Assasin:
                    hero.HealthPoints -= 50;
                    return $"{hero.Name}'s health points decreased by 50";
                case HeroClass.Cleric:
                    foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.Damage))
                    {
                        ability.AbilityPower -= 2;
                    }
                    return $"{hero.Name}'s damaging abilities decreased by 2";
                case HeroClass.Mage:
                    foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.Damage))
                    {
                        ability.AbilityPower += 5;
                    }
                    return $"{hero.Name}'s damaging abilities power increased by 5";
                default:
                    return string.Empty;
            }
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
