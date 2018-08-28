using System.Collections.Generic;
using Team8Project.Common.Enums;

namespace Team8Project.Contracts
{
    public interface IHero
    {
        string Name { get; set; }
        int HealthPoints { get; set; }
        int DmgStartOfRange { get; set; }
        int DmgEndOfRange { get; set; }
        IHero Opponent { get; set; }
        bool IsIncapacitated { get; set; }
        bool HasRessistance { get; set; }
        HeroClass HeroClass { get; set; }
        IList<IAbility> Abilities { get; set; }
        IList<IEffect> AppliedEffects { get; set; }
        void UseAbility(IAbility ability);
        void InitializeTerrain(ITerrain terrain);
    }
}
