namespace Team8Project.Contracts
{
    public interface IEffect : IAbility
    {
        int CurrentStacks { get; set; }
        int DefaultStacks { get; set; }
        string Affect();
        void Expire();
    }
}
