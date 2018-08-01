using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Common;

namespace Team8Project.Contracts
{
    public interface IDamagingAbility : IAbility
    {
        int SpellPower { get; set; }

    }
}
