using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Static;
using Team8Project.Contracts;
using Team8Project.Models.Magic;
using Team8Project.Common;

namespace Team8Project.Models
{
    public class Hero : IHero
    {
        private string name;
        private int healthPoints;
        private int dmgStartOfRange;
        private int dmgEndOfRange;
        private List<IAbility> abilities;
        private IHero oppopnent;
        private bool hasTurn;
        private HeroClass heroClass;


        public Hero(string name, int healthPoints, int dmgStartOfRange, int dmgEndOfRange, HeroClass heroClass)
        {
           this.Name = name;
            this.HealthPoints = healthPoints;
            this.DmgStartOfRange = dmgStartOfRange;
            this.DmgEndOfRange = dmgEndOfRange;
            this.HeroClass = heroClass;
            this.abilities = new List<IAbility>();
            this.hasTurn = false;

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
        public List<IAbility> Abilities { get { return this.abilities; } }
        public bool HasTurn
        {
            get { return this.hasTurn; }
            set { this.hasTurn = value; }
        }
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

        public void AddAbility(IAbility ability)
        {
            this.abilities.Add(ability);
        }

        public void UseAbility(IAbility ability)
        {
            ability.Incantation();
        }

        //Do not delete!
        //public void Cast(DamagingAbility ability)
        //{
        //    ability.Caster = this;

    }
}

