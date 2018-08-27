namespace Team8Project.IO.Contracts
{
    public interface IRenderer
    {
        void UpdataScreen();
        void InitialScreen();

        void SetScreenSize();
        void UpdateActiveHero();
        string[] CharacterSelection();
    }
}