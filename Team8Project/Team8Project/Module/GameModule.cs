using System;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Core;
using Team8Project.Core.Contracts;
using Team8Project.Data;
using Team8Project.IO.Contracts;
using Team8Project.IO;
using System.Reflection;
using Team8Project.Contracts;
using Team8Project.Core.Commands;
using Team8Project.Core.Managers;

namespace Team8Project.Module
{
    public class GameModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameEngine>().As<IEngine>().SingleInstance();
            builder.RegisterType<Factory>().As<IFactory>().SingleInstance();
            builder.RegisterType<TurnProcessor>().AsSelf().SingleInstance();
            builder.RegisterType<TerrainManager>().AsSelf().SingleInstance();
            builder.RegisterType<CommandProcessor>().AsSelf().SingleInstance();
            builder.RegisterType<DataContainer>().As<IDataContainer>().SingleInstance();
            builder.RegisterType<ConsoleReader>().As<IReader>().SingleInstance();
            builder.RegisterType<ConsoleWriter>().As<IWriter>().SingleInstance();
            builder.RegisterType<CommandProvider>().As<ICommandProvider>().SingleInstance();
            builder.RegisterType<ConsoleRenderer>().As<IRenderer>().SingleInstance();
            builder.RegisterType<ActManager>().As<IActManager>().SingleInstance();
            builder.RegisterType<AdvancedChecker>().AsSelf().SingleInstance();

            RegisterDynamicTerrains(builder);
            RegisterDynamicCommands(builder);
            base.Load(builder);
        }




        private static void RegisterDynamicTerrains(ContainerBuilder builder)
        {

            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            var terrainTypes = currentAssembly.DefinedTypes
                .Where(typeInfo =>
                    typeInfo.ImplementedInterfaces.Contains(typeof(ITerrain)) && typeInfo.IsAbstract == false)
                .ToList();
            
            foreach (var terrainType in terrainTypes)
            {
                builder.RegisterType(terrainType.AsType())
                  .Named<ITerrain>(
                    terrainType.Name.ToLower());
            }
        }

        private static void RegisterDynamicCommands(ContainerBuilder builder)
        {

            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            var commandTypes = currentAssembly.DefinedTypes
                .Where(typeInfo =>
                    typeInfo.ImplementedInterfaces.Contains(typeof(ICommand)) && typeInfo.IsAbstract == false)
                .ToList();

            // register in autofac
            foreach (var commandType in commandTypes)
            {
                builder.RegisterType(commandType.AsType())
                  .Named<ICommand>(
                    commandType.Name.ToLower().Replace("command", ""));
            }
        }
    }
}
