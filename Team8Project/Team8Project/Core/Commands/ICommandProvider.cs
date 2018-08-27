namespace Team8Project.Core.Commands
{
    public interface ICommandProvider
    {
        ICommand GetCommand(string commandName);
    }
}