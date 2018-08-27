using System.Linq;
using Team8Project.Common;
using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Models.Magic.EffectAbilities
{
    public abstract class Effect : Ability, IEffect
    {
        private int currentStacks;
        private int defaultStacks;
        private IHero target;

        public Effect(string name, int cd, HeroClass heroClass, EffectType type, int defaultStacks, int abilityPower)
            : base(name, cd, heroClass, type, abilityPower)
        {
            this.DefaultStacks = defaultStacks;
        }

        protected IHero Target
        {
            get { return target; }
            set
            {
                target = value;
            }
        }
        public int CurrentStacks
        {
            get { return this.currentStacks; }
            set
            {
                Validations.ValidateRangeNumbers(value, Constants.MIN_CD, Constants.MAX_CD, $"Current stacks value is out of range {Constants.MIN_CD} - {Constants.MAX_CD}");
                this.currentStacks = value;
            }
        }

        public int DefaultStacks
        {
            get { return defaultStacks; }
            set
            {
                Validations.ValidateRangeNumbers(value, Constants.MIN_CD, Constants.MAX_CD, $"Default stacks value is out of range {Constants.MIN_CD} - {Constants.MAX_CD}");
                defaultStacks = value;
            }
        }

        public override void Apply()
        {

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

        public abstract string Affect();
        public virtual void Expire()
        {
            this.Target.AppliedEffects.Remove(this);
        }



        public override string ToString()
        {
            return $"applies {this.Type.ToString()} with {this.CurrentStacks} stacks";
        }

        public override string Print()
        {
            return base.Print();

        }
    }
}
