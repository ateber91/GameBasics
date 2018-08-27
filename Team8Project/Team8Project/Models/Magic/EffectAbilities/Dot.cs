using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Models.Magic.EffectAbilities
{
    public class Dot : Effect, IEffect
    {
        public Dot(string name, int cd, HeroClass heroClass, EffectType type, int defaultStacks, int abilityPower) : base(name, cd, heroClass, type, defaultStacks, abilityPower)
        {
        }

        public override void Apply()
        {
            base.Target = base.Caster.Opponent;
            base.Apply();
        }

        public override string Affect()
        {
            this.Target.HealthPoints -= this.AbilityPower;
            this.CurrentStacks--;
            if (CurrentStacks == 0)
            {
                this.Expire();
            }
            return ($"{this.Target.Name} takes {this.AbilityPower}dmg from {this.Name}");
        }

        public override string Print()
        {
            var sb = new StringBuilder();
          //  sb.Append($"{this.Caster.Name} applies {this.Name} on opponent and does {this.AbilityPower}dmg/turn.");
            sb.Append($"{this.Name} {this.AbilityPower}dmg/turn.");
            sb.Append(base.Print());
            return sb.ToString();

        }

    }
}
