namespace Team8Project.Contracts
{
    public interface ITerrain
    {
        string HeroEffect(IHero hero);
        string ContinuousEffect(IHero hero);
        bool IsDay { get; set; }
    }
}
