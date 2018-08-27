using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Models.Magic.EffectAbilities
{
    public class Hot : Effect, IEffect
    {
        public Hot(string name, int cd, HeroClass heroClass, EffectType type, int defaultStacks, int abilityPower) : base(name, cd, heroClass, type, defaultStacks, abilityPower)
        {
        }

        public override void Apply()
        {
            base.Target = base.Caster;
            base.Apply();

        }

        public override string Affect()
        {
            this.Target.HealthPoints += this.AbilityPower;
            this.CurrentStacks--;
            if (CurrentStacks == 0)
            {
                this.Expire();
            }
            return ($"{this.Target.Name} heals {this.AbilityPower}dmg from {this.Name}");
        }

        public override string Print()
        {
            var sb = new StringBuilder();
            //sb.Append($"{this.Caster.Name} applies {this.Name} on self and heals {this.AbilityPower}hp/turn.");
            sb.Append($"{this.Name} heals {this.AbilityPower}hp/turn.");
            sb.Append(base.Print());
            return sb.ToString();

        }

    }
}
