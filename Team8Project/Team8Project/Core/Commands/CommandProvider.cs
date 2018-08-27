using Autofac;
using Team8Project.Core.Contracts;

namespace Team8Project.Core.Commands
{
    public class CommandProvider : ICommandProvider
    {
        private readonly IComponentContext componentContext;

        public CommandProvider(IComponentContext context)
        {
            this.componentContext = context;
        }

        public ICommand GetCommand(string commandName)
        {
            return this.componentContext.ResolveNamed<ICommand>(commandName.ToLower());
        }
    }
}
