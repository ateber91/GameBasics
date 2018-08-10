using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team8Project.Contracts
{
    public interface IEffect : IAbility
    {
        int CurrentStacks { get; set; }
        int DefaultStacks { get; set; }
    }
}
