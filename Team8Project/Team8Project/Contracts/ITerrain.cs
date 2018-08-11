namespace Team8Project.Contracts
{
    public interface ITerrain
    {
        void HeroEffect(IHero hero);
        void ContinuousEffect(IHero hero);
        bool IsDay { get; set; }
        string ToString();
    }
}
