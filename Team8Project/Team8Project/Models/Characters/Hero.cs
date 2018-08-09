using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Static;
using Team8Project.Contracts;
using Team8Project.Models.Magic;
using Team8Project.Common;
using Team8Project.Models.Statuses;

namespace Team8Project.Models
{
    public class Hero : IHero
    {
        private string name;
        private int healthPoints;
        private int dmgStartOfRange;
        private int dmgEndOfRange;
        private IList<IAbility> abilities;
        private IHero oppopnent;
        private HeroClass heroClass;
        private IList<Status> status;
        private IList<EffectType> currentEffects;

        public IList<Status> Status { get { return this.status; } set { this.status = value; } }
        public IList<EffectType> CurrentEffects { get { return this.currentEffects; } set { this.currentEffects = value; } }

        public Hero()
        {
            this.Abilities = new List<IAbility>();
            this.Status = new List<Status>();
            this.currentEffects = new List<EffectType>();
        }

        public string Name { get => this.name; set => this.name = value; }
        public int HealthPoints
        {
            get { return this.healthPoints; }
            set { this.healthPoints = value; }
        }
        public int DmgStartOfRange
        {
            get { return this.dmgStartOfRange; }
            set { this.dmgStartOfRange = value; }
        }
        public int DmgEndOfRange
        {
            get { return this.dmgEndOfRange; }
            set { this.dmgEndOfRange = value; }
        }
        public IList<IAbility> Abilities { get { return this.abilities; } set { this.abilities = value; } } //?leave or remove set depending on future game logic
        
        public IHero Opponent
        {
            get { return this.oppopnent; }
            set { this.oppopnent = value; }
        }

        public HeroClass HeroClass
        {
            get { return this.heroClass; }
            set { this.heroClass = value; }
        }

        public void UseAbility(IAbility ability)
        {
            ability.ApplyAbility();
        }

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

            //if (this.Status.Count == 0)
            //{
            //    sb.AppendLine("NO EFFECTS CURRENTLY");
            //}
            //else
            //{
            //    sb.AppendLine("CURRENT EFFECTS:");
            //    foreach (EffectType effect in this.CurrentEffects)
            //    {
            //        sb.Append(effect);
            //    }
            //}
            return sb.ToString();
        }

    }
}

