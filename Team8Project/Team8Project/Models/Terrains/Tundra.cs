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
                    GameEngine.Instance.Log.AppendLine("DECREASED WARRIORS HP BY 25");
                    break;
                case HeroClass.Assasin:
                    hero.HealthPoints -= 50;
                    GameEngine.Instance.Log.AppendLine("DECREASED ASSASINS HP BY 50");
                    break;
                case HeroClass.Cleric:
                    foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.Damage))
                    {
                        ability.AbilityPower -= 2;
                        GameEngine.Instance.Log.AppendLine("DECREASED ALL OF CLERICSC DAMAGING ABILITY ATTACK POWER BY 2");
                    }
                    break;
                case HeroClass.Mage:
                    foreach (var ability in hero.Abilities.Where(x => x.Type == EffectType.Damage))
                    {
                        ability.AbilityPower += 5;
                        GameEngine.Instance.Log.AppendLine("INCREASD ALL OF MAGES DAMAGING ABILITIES ATTACK POWER BY 5");
                    }
                    break;
            }
        }
        public override void ContinuousEffect(IHero hero)
        {
            //TODO FIX!
            if (this.IsDay)
            {
                if (hero.AppliedEffects.Count != 0)
                {
                    var effects = hero.AppliedEffects;

                    effects
                        .Where(e => e.Type == EffectType.Incapacitated)
                        .ToList()
                        .ForEach(e => e.CurrentStacks++);
                    GameEngine.Instance.Log.AppendLine("INCREASSED DURATION OF ALL INCAPACITATING EFFECTS");
                }
            }
            else
            {
                foreach (var ability in hero.Abilities.Where(x=>x.Type==EffectType.Damage))
                {
                    if (ability.OnCD == true)
                    {
                        ability.OnCD = false;
                        GameEngine.Instance.Log.AppendLine(ability.Name + " ABILITY COOLDOWN CHANGED");
                    }
                    else
                    {
                        var effects = hero.Abilities;

                        effects
                            .Where(e => e.Type == EffectType.HOT)
                            .ToList()
                            .ForEach(e => e.AbilityPower++);
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();


            if (!this.IsDay)
            {
                //sb.AppendLine("Incapacitating effects extended with 1 turn");
            }
            else
            {
                //sb.AppendLine("Hero max damage reduced by 2");
            }
            return sb.ToString();
        }
    }
}
