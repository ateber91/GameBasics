using System.Collections.Generic;
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
        IList<IAbility> Abilities { get; set; } 
        IList<IEffect> AppliedEffects { get; set; }
        void UseAbility(IAbility ability);
    }
}
