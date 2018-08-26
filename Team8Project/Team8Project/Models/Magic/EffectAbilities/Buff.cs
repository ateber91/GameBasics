using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Models.Magic.EffectAbilities
{
    public class Buff : Effect, IEffect
    {
        private int valueToBeRestored;
        public Buff(string name, int cd, HeroClass heroClass, EffectType type, int defaultStacks, int abilityPower) : base(name, cd, heroClass, type, defaultStacks, abilityPower)
        {
        }

        public override void Apply()
        {
            this.Target = base.Caster;
            base.Apply();
            this.valueToBeRestored = this.AbilityPower;
            this.Target.DmgStartOfRange += this.AbilityPower;
            this.Target.DmgEndOfRange += this.AbilityPower;

        }

        public override string Affect()
        {

            this.CurrentStacks--;
            if (CurrentStacks < 0)
            {
                this.Expire();
            }

            return string.Empty; //no msg to be desplayed
        }

        public override void Expire()
        {
            this.Target.DmgStartOfRange -= this.valueToBeRestored;
            this.Target.DmgEndOfRange -= this.valueToBeRestored;
            base.Expire();
        }

        public override string Print()
        {
            var sb = new StringBuilder();
           // sb.Append($"{this.Caster.Name} applies {this.Type} on self and adds {this.AbilityPower} to attack damage");
            sb.Append($"{this.Name} {this.AbilityPower} dmg increase.");
            sb.Append(base.Print());
            return sb.ToString();

        }

    }
}
