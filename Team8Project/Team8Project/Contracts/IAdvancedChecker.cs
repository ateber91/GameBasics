namespace Team8Project.Core.Contracts
{
    public interface IAdvancedChecker
    {
        bool CheckForIncapacitation();
        void CheckForTerrainContitiousEffect();
        string CheckIfabilityInputIsValid(string abilityHotKey);
        bool CheckIfGameIsOver();
        void SetAbilityThatIsReadyForUse();
    }
}