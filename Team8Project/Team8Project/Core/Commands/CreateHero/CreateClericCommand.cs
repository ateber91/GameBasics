using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Data;

namespace Team8Project.Core.Commands.CreateHero
{
    class CreateClericCommand : Command, ICommand
    {
        public CreateClericCommand(IFactory factory, IDataContainer data) : base(factory, data)
        {
        }

        public override void Execute()
        {
            string name = "Genka";
            HeroClass heroClass = HeroClass.Cleric;
            int healthPoints = 100;
            int dmgStartOfRange = 8;
            int dmgEndOfRange = 10;

            IHero hero = this.factory.CreateCleric(name, heroClass, healthPoints, dmgStartOfRange, dmgEndOfRange);
            this.data.ListHeros.Add(hero);
        }
    }
}
