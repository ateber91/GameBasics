using Team8Project.Common.Enums;

namespace Team8Project.Contracts
{
    public interface IAbility
    {
        string Name { get; }
        int AbilityPower { get; set; }
        int Cd { get; set; }
        bool OnCD { get; set; }
        int CDCounter { get; set; }
        IHero Caster { get; set; }
        IHero Target { get; set; }
        HeroClass HeroClass { get; }
        EffectType Type { get; set; }
        void Apply();
        string Print();
    }
}
