using Autofac;
using System.Reflection;
using Team8Project.Core;
using Team8Project.Core.Contracts;

namespace Team8Project
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());



            var container = builder.Build();
            var engine = container.Resolve<IEngine>();

            engine.Run();
        }
    }
}