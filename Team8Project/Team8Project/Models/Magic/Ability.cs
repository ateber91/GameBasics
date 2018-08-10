using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Common;
using Team8Project.Contracts;

namespace Team8Project.Models.Magic
{
    public abstract class Ability : IAbility
    {
        private string name;
        private int cd;
        private int abilityPower;
        private IHero caster;
        private IHero target;
        private HeroClass heroClass;
        private EffectType type;
        private bool onCD = false;
        private int cDCounter = 0;

        public int CDCounter
        {
            get
            {
                return this.cDCounter;
            }
            set
            {
                //TODO: Validation (Needs to be discussed with Georgi Z)
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
                Validations.ValidateLength(value, 1, 60, "The name of your ability can be less than 1 and more than 60 characters");
                this.name = value;
            }
        }
        public int Cd
        {
            get { return this.cd; }
            set
            {
                //TODO: Validation (Needs to be discussed with Georgi Z)
                this.cd = value;
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

        public IHero Target
        {
            get { return target; }
            set
            {
                target = value;
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

