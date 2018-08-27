using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Data;

namespace Team8Project.Core.Commands.SelectAbility
{
    public class SelectEffectAbilityCommand : Command, ICommand
    {
        private readonly TurnProcessor turn;

        public SelectEffectAbilityCommand(IFactory factory, IDataContainer data, TurnProcessor turn) : base(factory, data)
        {
            this.turn = turn;
        }

        public override void Execute()
        {
            this.data.SelectedAbility = turn.ActiveHero.Abilities[2];
        }
    }
}
