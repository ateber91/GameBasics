using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Models.Magic.EffectAbilities
{
    public class Incapacitation : Effect, IEffect
    {
        public Incapacitation(string name, int cd, HeroClass heroClass, EffectType type, int defaultStacks, int abilityPower) : base(name, cd, heroClass, type, defaultStacks, abilityPower)
        {
        }

        public override void Apply()
        {
            base.Target = base.Caster.Opponent;
            base.Target.IsIncapacitated = true;
            base.Apply();
        }

        public override string Affect()
        {
            this.Target.IsIncapacitated = true;
            this.CurrentStacks--;
            if (CurrentStacks == 0) 
            {
                this.Expire();
            }
            this.Expire();
            return ($"{this.Target.Name} has applied {this.Name}, cannot act this turn!");
        }
        public override void Expire()
        {
            this.Target.IsIncapacitated = false;
            base.Expire();
        }
        public override string Print()
        {
            var sb = new StringBuilder();
            //  sb.Append($"{this.Caster.Name} applies {this.Name} on {this.Caster.Opponent}");
            sb.Append($"{this.Name} incapacitate opponent for 1 turn.");
            sb.Append(base.Print());
            return sb.ToString();

        }
    }
}
