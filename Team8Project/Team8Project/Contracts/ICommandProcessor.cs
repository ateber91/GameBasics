namespace Team8Project.Core.Commands
{
    public interface ICommandProcessor
    {
        void ProcessCommand(string key);
        void ProcessCommand(string[] players);
    }
}