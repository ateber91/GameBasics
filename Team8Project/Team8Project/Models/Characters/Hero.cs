using System;
using System.Collections.Generic;
using System.Text;
using Team8Project.Common;
using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Models
{
    public abstract class Hero : IHero
    {
        private string name;
        private int healthPoints;
        private int dmgStartOfRange;
        private int dmgEndOfRange;
        private IList<IAbility> abilities;
        private IHero oppopnent;
        private HeroClass heroClass;
        private IList<IEffect> appliedEffects;
       
        public Hero(string name, HeroClass heroClass, int healthPoints, int dmgStartOfRange, int dmgEndOfRange)
        {
            this.Name = name;
            this.HeroClass = heroClass;
            this.HealthPoints = healthPoints;
            this.DmgStartOfRange = dmgStartOfRange;
            this.DmgEndOfRange = dmgEndOfRange;
            this.Abilities = new List<IAbility>();
            this.AppliedEffects = new List<IEffect>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }
             set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Your hero name is empty or null.");
                }
                Validations.ValidateLength(value, Constants.NAME_MIN_LEN, Constants.NAME_MAX_LEN, $"The name of your hero can be between {Constants.NAME_MIN_LEN} and {Constants.NAME_MAX_LEN} symbols!");
                this.name = value;
            }
        }
        public int HealthPoints
        {
            get { return this.healthPoints; }
            set
            {
                this.healthPoints = value;
            }
        }
        public int DmgStartOfRange
        {
            get { return this.dmgStartOfRange; }
            set
            {
                Validations.ValidateRangeNumbers(value, Constants.HP_MIN, Constants.HP_MAX, $"The damage ability of your hero can't be less than {Constants.HP_MIN} and more than {Constants.HP_MAX}");
                this.dmgStartOfRange = value;
            }
        }
        public int DmgEndOfRange
        {
            get { return this.dmgEndOfRange; }
            set
            {
                Validations.ValidateRangeNumbers(value, this.DmgStartOfRange, Constants.HP_MAX, $"The damage ability of your hero can't be less than {this.DmgStartOfRange} and more than {Constants.HP_MAX}");
                this.dmgEndOfRange = value;
            }
        }

        public IList<IAbility> Abilities
        {
            get { return this.abilities; }
            set
            { this.abilities = value; }
        }

        public IHero Opponent
        {
            get { return this.oppopnent; }
            set { this.oppopnent = value ?? throw new ArgumentNullException(); }
        }

        public HeroClass HeroClass
        {
            get { return this.heroClass; }
            set { this.heroClass = value; }
        }

        public IList<IEffect> AppliedEffects
        {
            get { return this.appliedEffects; }
            set
            {
                this.appliedEffects = value;
            }
        }

        public bool IsIncapacitated { get; set; }
        public bool HasRessistance { get; set; }

        public void UseAbility(IAbility ability)
        {
            ability.Apply();
        }

        public abstract void InitializeTerrain(ITerrain terrain);

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Hero health points: " + this.HealthPoints);
            sb.AppendLine("Hero damage " + this.DmgStartOfRange + " to " + this.DmgEndOfRange);
            sb.AppendLine("Hero class " + this.HeroClass);
            sb.AppendLine("Spells:");
            foreach (IAbility spell in this.Abilities)
            {
                sb.Append(spell.Print());
            }
            return sb.ToString();
        }

    }
}

