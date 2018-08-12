using System.Text;
using Team8Project.Common;
using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Models.Magic
{
    public abstract class Ability : IAbility
    {
        private string name;
        private int cd;
        private int abilityPower;
        private IHero caster;
        private HeroClass heroClass;
        private EffectType type;
        private bool onCD = false;
        private int cDCounter;
        private const int MIN_NAME_LEN = 1;
        private const int MAX_NAME_LEN = 60;

        public Ability(string name, int cd, HeroClass heroClass, EffectType type, int abilityPower)
        {
            this.Name = name;
            this.Cd = cd;
            this.HeroClass = heroClass;
            this.AbilityPower = abilityPower;
            this.Type = type;

        }

        public string Name
        {
            get { return this.name; }
            private set
            {
                Validations.ValidateLength(value, MIN_NAME_LEN, MAX_NAME_LEN, $"The name of your ability can be less than {MIN_NAME_LEN} and more than {MAX_NAME_LEN} characters");
                this.name = value;
            }
        }
        public int Cd
        {
            get { return this.cd; }
            set
            {
                this.cd = value;
            }
        }
        public int CDCounter
        {
            get
            {
                return this.cDCounter;
            }
            set
            {
                this.cDCounter = value;
            }
        }

        public bool OnCD
        {
            get
            {
                return this.onCD;
            }
            set
            {
                this.onCD = value;
            }
        }
        public IHero Caster
        {
            get { return this.caster; }
            set
            {
                this.caster = value;
            }
        }

        public HeroClass HeroClass
        {
            get { return this.heroClass; }
            private set
            {
                this.heroClass = value;
            }
        }

        public EffectType Type
        {
            get { return this.type; }
            set
            {
                this.type = value;
            }
        }

        public int AbilityPower
        {
            get { return abilityPower; }
            set
            {
                abilityPower = value;
            }
        }


        public virtual void Apply()
        {
            this.CDCounter = -1;
            this.OnCD = true;
        }

        public virtual string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((this.OnCD) ? $" On cooldown {this.Cd - this.CDCounter} turns" : "");
            return sb.ToString();
        }
    }
}

