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
    public class CreateWarriorCommand : Command, ICommand
    {
        public CreateWarriorCommand(IFactory factory, IDataContainer data) : base(factory, data)
        {
        }

        public override void Execute()
        {
            string name = "Gesho";
            HeroClass heroClass = HeroClass.Warrior;
            int healthPoints = 150;
            int dmgStartOfRange = 15;
            int dmgEndOfRange = 20;

            IHero hero = this.factory.CreateWarrior(name, heroClass, healthPoints, dmgStartOfRange, dmgEndOfRange);
            this.data.ListHeros.Add(hero);
        }
    }
}
