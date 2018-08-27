using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Models.Magic.EffectAbilities
{
    public class Debuff : Effect, IEffect
    {
        private int valueToBeRestored;
        public Debuff(string name, int cd, HeroClass heroClass, EffectType type, int defaultStacks, int abilityPower) : base(name, cd, heroClass, type, defaultStacks, abilityPower)
        {
        }

        public override void Apply()
        {
            this.Target = base.Caster.Opponent;
            base.Apply();
            this.valueToBeRestored = this.AbilityPower;
            this.Target.DmgStartOfRange -= this.AbilityPower;
            this.Target.DmgEndOfRange -= this.AbilityPower;
        }

        public override string Affect()
        {
            this.CurrentStacks--;
            if (CurrentStacks < 0)
            {
                this.Expire();
            }
            return string.Empty; // no msg to be displayed
        }

        public override void Expire()
        {
            this.Target.DmgStartOfRange += this.valueToBeRestored;
            this.Target.DmgEndOfRange += this.valueToBeRestored;
            base.Expire();
        }

        public override string Print()
        {
            var sb = new StringBuilder();
        //    sb.Append($"{this.Caster.Name} applies {this.Name} on {this.Caster.Opponent} and removes {this.AbilityPower} from attack damage");
            sb.Append($"{this.Name} {this.AbilityPower} dmg reduction.");
            sb.Append(base.Print());
            return sb.ToString();
        }

    }
}
