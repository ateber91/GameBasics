using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Contracts;
using Team8Project.Core;

namespace Team8Project.Models.Statuses
{
    public class Frozen : Status
    {
        protected Frozen(string name, bool thisTurnApplied, int duration, IHero caster) : base(name, thisTurnApplied, duration, caster)
        {
        }

        protected override void Apply()
        {

        }

        protected override void Expire()
        {
            throw new NotImplementedException();
        }
    }
}
