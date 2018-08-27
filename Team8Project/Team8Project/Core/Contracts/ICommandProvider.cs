namespace Team8Project.Core.Contracts
{
    public interface ICommandProvider
    {
        ICommand GetCommand(string commandName);
    }
}