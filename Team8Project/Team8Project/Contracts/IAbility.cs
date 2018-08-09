namespace Team8Project.Contracts
{
    public interface IAbility
    {
        string Name { get; set; }
        int Cd { get; set; }
        IHero Caster { get; set; }
        string Print();
        void ApplyAbility();
    }
}
