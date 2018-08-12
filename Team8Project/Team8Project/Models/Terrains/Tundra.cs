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

        public override void HeroEffect(IHero hero)
        {
            switch (hero.HeroClass)
            {
                case HeroClass.Warrior:
                    hero.HealthPoints -= 25;
                    GameEngine.Instance.Log.AppendLine(hero.Name + "'s healthpoints decreased by 25");
                    break;
                case HeroClass.Assasin:
                    hero.HealthPoints -= 50;
                    GameEngine.Instance.Log.AppendLine(hero.Name + "'s health points decreased by 50");
                    break;
                case HeroClass.Cleric:
                    foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.Damage))
                    {
                        ability.AbilityPower -= 2;
                    }
                    GameEngine.Instance.Log.AppendLine(hero.Name + "'s damaging abilities decreased by 2");
                    break;
                case HeroClass.Mage:
                    foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.Damage))
                    {
                        ability.AbilityPower += 5;
                    }
                    GameEngine.Instance.Log.AppendLine(hero.Name + "'s damaging abilities power increased by 5");
                    break;
            }
        }
        public override void ContinuousEffect(IHero hero)
        {
            if (this.IsDay == false)
            {
                foreach (var ability in hero.Abilities.Skip(1))
                {
                    if (ability.OnCD == false)
                    {
                        ability.OnCD = true;
                        ability.CDCounter = ability.Cd - 1;
                        GameEngine.Instance.Log.AppendLine(ability.Name + "'s is now on cooldown");
                    }
                }
            }
            else
            {
                if (hero.AppliedEffects.Count != 0)
                {
                    var effects = hero.AppliedEffects;

                    effects
                        .Where(e => e.Type == EffectType.Incapacitated)
                        .ToList()
                        .ForEach(e => e.CurrentStacks++);
                    GameEngine.Instance.Log.AppendLine("'s incapacitating effects' duration increased by 1");
                }
                else
                {
                    GameEngine.Instance.Log.AppendLine("He was safe");
                }
            }
        }
    }
}
