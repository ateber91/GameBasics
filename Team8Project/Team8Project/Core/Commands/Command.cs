using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Data;

namespace Team8Project.Core.Commands
{
    public abstract class Command : ICommand
    {
        protected IFactory factory;
        protected IDataContainer data;
        public Command(IFactory factory, IDataContainer data)
        {
            this.factory = factory;
            this.data = data;
        }
        public abstract void Execute();
    }
}
