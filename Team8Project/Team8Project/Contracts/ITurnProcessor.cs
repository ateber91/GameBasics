using Team8Project.Contracts;

namespace Team8Project.Core.Contracts
{
    public interface ITurnProcessor
    {
        IHero ActiveHero { get; set; }
        int TurnNumber { get; }

        void NextTurn();
        void SetActiveHero();
        void SetFirstTurn();
        void UpdateCooldowns(IHero activeHero);
    }
}