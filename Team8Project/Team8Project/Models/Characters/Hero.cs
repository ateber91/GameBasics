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

        public Hero()
        {
            this.Abilities = new List<IAbility>();
           
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

    }
}

