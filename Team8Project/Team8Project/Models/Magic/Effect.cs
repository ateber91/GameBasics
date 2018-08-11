﻿using System.Linq;
using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Models.Magic
{
    public class Effect : Ability, IEffect
    {
        private int currentStacks;
        private int defaultStacks;

        public Effect(string name, int cd, /*IHero caster, */HeroClass heroClass, EffectType type, int defaultStacks, int abilityPower)
            : base(name, cd, /*caster,*/ heroClass, type, abilityPower)
        {
            this.DefaultStacks = defaultStacks;
        }

        public int CurrentStacks
        {
            get { return this.currentStacks; }
            set
            {
                this.currentStacks = value;
            }
        }

        public int DefaultStacks
        {
            get { return defaultStacks; }
            set
            {
                defaultStacks = value;
            }
        }

        public override void Apply()
        {
            var target = GetTarget();

            if (target.AppliedEffects.Contains(this))
            {
                target.AppliedEffects.FirstOrDefault(x => x == this).CurrentStacks = currentStacks + DefaultStacks;
            }
            else
            {
                this.CurrentStacks = this.DefaultStacks;
                target.AppliedEffects.Add(this);
            }
            base.Apply();
        }

        private IHero GetTarget()
        {
            if (this.Type == EffectType.Buff || this.Type == EffectType.HOT || this.Type == EffectType.Resistance) { return this.Caster; }
            return this.Caster.Opponent;
        }

        public override string ToString()
        {
            return $"applies {this.Type.ToString()} with {this.CurrentStacks} stacks";
        }

        public override string Print()
        {
            var hotOrDot = this.Type == EffectType.HOT ? "hp/turn" : "dmg/turn";
            var effect = this.AbilityPower == 0 ? string.Empty : $"{this.AbilityPower.ToString()} {hotOrDot}";
            var target = this.GetTarget() == this.Caster ? "caster" : "opponent";
            var sb = new StringBuilder();
            sb.Append($"{this.Name} applies {this.Type} {effect} on {target}");
            sb.Append(base.Print());
            return sb.ToString();
        }

    }
}

