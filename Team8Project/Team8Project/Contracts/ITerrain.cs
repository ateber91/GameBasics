namespace Team8Project.Contracts
{
    public interface ITerrain
    {
        string ContinuousEffect(IHero hero);
        bool IsDay { get; set; }
        void ApplyInitialWarriorEffect(IHero hero);
        void ApplyInitialAssasinEffect(IHero hero);
        void ApplyInitialClericEffect(IHero hero);
        void ApplyInitialMageEffect(IHero hero);
    }
}
