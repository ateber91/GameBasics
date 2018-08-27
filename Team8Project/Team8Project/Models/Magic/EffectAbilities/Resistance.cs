using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Models.Magic.EffectAbilities
{
    public class Resistance : Effect, IEffect
    {
        public Resistance(string name, int cd, HeroClass heroClass, EffectType type, int defaultStacks, int abilityPower) : base(name, cd, heroClass, type, defaultStacks, abilityPower)
        {
        }
        public override void Apply()
        {
            base.Target = base.Caster;
            Target.HasRessistance = true;
            base.Apply();
        }

        public override string Affect()
        {
            this.Target.HasRessistance = true;
            this.CurrentStacks--;
            if (CurrentStacks == 0)            {
                this.Expire();
                return string.Empty;
            }
            else
            {
                return ($"{this.Target.Name} has {this.Name}, all damage taken will be 0");
            }
        }

        public override void Expire()
        {
            this.Target.HasRessistance = false;
            base.Expire();
        }
        public override string Print()
        {
            var sb = new StringBuilder();
            //    sb.Append($"{this.Caster.Name} applies {this.Name} on self.");
            sb.Append($"{this.Name} blocks incoming dmg");
            sb.Append(base.Print());
            return sb.ToString();

        }
    }
}
