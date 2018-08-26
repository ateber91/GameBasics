namespace Team8Project.Contracts
{
    public interface ITerrain
    {
        void ApplyInitialEffect(IHero hero);
        string ContinuousEffect(IHero hero);
        bool IsDay { get; set; }
    }
}
