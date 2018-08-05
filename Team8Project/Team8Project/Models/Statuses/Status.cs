using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Contracts;

namespace Team8Project.Models.Statuses
{
    public abstract class Status
    {
        private string name;
        private bool thisTurnApplied;
        private int duration;
        private IHero caster;

        protected Status(string name, bool thisTurnApplied, int duration, IHero target)
        {
            Name = name;
            ThisTurnApplied = thisTurnApplied;
            Duration = duration;
            Caster = caster;
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }
        public bool ThisTurnApplied
        {
            get { return thisTurnApplied; }
            set
            {
                thisTurnApplied = value;
            }
        }
        public int Duration
        {
            get { return duration; }
            set
            {
                duration = value;
            }
        }
        public IHero Caster
        {
            get { return caster; }
            set
            {
                caster = value;
            }
        }

        protected abstract void Apply();
        protected abstract void Expire();



        private void Affect()
        {
            if (this.ThisTurnApplied == true)
            {
                Apply();
                ThisTurnApplied = false;
            }
            else if (Duration == 0)
            {
                Expire();
            }
            Duration--;
        }
    }
}

