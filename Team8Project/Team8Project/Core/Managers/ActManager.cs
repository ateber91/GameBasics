using System.Linq;
using Team8Project.Contracts;
using Team8Project.Data;
using Team8Project.IO.Contracts;

namespace Team8Project.Core.Managers
{
    public class ActManager : IActManager
    {
        private readonly AdvancedChecker checker;
        private readonly IReader reader;
        private readonly IRenderer renderer;
        private readonly CommandProcessor commandProcessor;
        private readonly TurnProcessor turn;
        private readonly IDataContainer data;

        public ActManager(AdvancedChecker checker, IRenderer renderer, IReader reader,
            CommandProcessor commandProcessor, TurnProcessor turn, IDataContainer data)
        {
            this.checker = checker;
            this.renderer = renderer;
            this.commandProcessor = commandProcessor;
            this.turn = turn;
            this.data = data;
            this.reader = reader;
        }

        public void Act(IHero activeHero)
        {
            if (this.checker.CheckForIncapacitation() == false)
            {
                foreach (var effect in turn.ActiveHero.AppliedEffects.ToList())
                {
                    var resultToBeLogged = effect.Affect();
                    if (resultToBeLogged != string.Empty) { this.data.Log.AppendLine(resultToBeLogged); }
                }

                this.renderer.UpdataScreen();
                this.renderer.UpdateActiveHero();

                var selectAbilityKey = this.checker.CheckIfabilityInputIsValid(this.reader.ConsoleReadKey()); //read from input

                this.commandProcessor.ProcessCommand(selectAbilityKey); // execute command
                this.checker.SetAbilityThatIsReadyForUse();

                turn.ActiveHero.UseAbility(this.data.SelectedAbility);

                this.data.Log.AppendLine($"{turn.ActiveHero.Name} uses {this.data.SelectedAbility.Name} and {this.data.SelectedAbility.ToString()}.");

                this.data.EndGame = this.checker.CheckIfGameIsOver();
                this.renderer.UpdataScreen();
                this.EndAct();
            }
        }
        private void EndAct()
        {
            turn.ActiveHero = turn.ActiveHero.Opponent;
        }

    }
}
