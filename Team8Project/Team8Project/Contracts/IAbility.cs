using Team8Project.Common;

namespace Team8Project.Contracts
{
    public interface IAbility
    {
        string Name { get; set; }
        int AbilityPower { get; set; }
        int Cd { get; set; }
        IHero Caster { get; set; }
        IHero Target { get; set; }
        HeroClass HeroClass { get; set; }
        EffectType Type { get; set; }
        void Apply();
        string Print();
    }
}
