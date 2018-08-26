using System;
using System.Collections.Generic;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core.Commands;
using Team8Project.Data;

namespace Team8Project.Core
{
    public class CommandProcessor
    {
        private IFactory factory;
        private TurnProcessor turn;
        private readonly IDataContainer data;
        private readonly ICommandProvider commandProvider;
        private Dictionary<string, string> heroSelection;

        public CommandProcessor(IFactory factory, TurnProcessor turn, IDataContainer data, ICommandProvider commandProvider)
        {
            this.factory = factory;
            this.turn = turn;
            this.data = data;
            this.commandProvider = commandProvider;
            this.heroSelection = new Dictionary<string, string>()
            {
                { "1", "CreateAssasin"},
                { "2", "CreateWarrior"},
                { "3", "CreateMage"},
                { "4", "CreateCleric"},
            };
        }


        public void ProcessCommand(string[] players)
        {
            foreach (var player in players)
            {
                var command = this.commandProvider.GetCommand(heroSelection[player].ToLower());
                command.Execute();
            }
        }

        public IAbility ProcessCommand(string key)
        {
            switch (key)
            {
                case "1": return turn.ActiveHero.Abilities[0];
                case "2": return turn.ActiveHero.Abilities[1];
                case "3": return turn.ActiveHero.Abilities[2];
                default: throw new ArgumentException("I couldn't select ability! :(");
            }
        }
    }
}
