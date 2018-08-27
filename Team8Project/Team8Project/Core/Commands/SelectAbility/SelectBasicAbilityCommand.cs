﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Data;

namespace Team8Project.Core.Commands.SelectAbility
{
    public class SelectBasicAbilityCommand : Command, ICommand
    {
        private readonly TurnProcessor turn;

        public SelectBasicAbilityCommand(IFactory factory, IDataContainer data, TurnProcessor turn) : base(factory, data)
        {
            this.turn = turn;
        }

        public override void Execute()
        {
            this.data.SelectedAbility = turn.ActiveHero.Abilities[0];
        }
    }
}