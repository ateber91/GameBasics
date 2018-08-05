using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Common;

namespace Team8Project.Contracts
{
    public interface IHero
    {
        string Name { get; set; }
        int HealthPoints { get; set; }
        int DmgStartOfRange { get; set; }
        int DmgEndOfRange { get; set; }
        IHero Opponent { get; set; }
        HeroClass HeroClass { get; set; }
        bool HasTurn { get; set; }
        IList<IAbility> Abilities { get; set; } //leave set>?
        void AddAbility(IAbility ability);
        void UseAbility(IAbility ability);
    }
}
