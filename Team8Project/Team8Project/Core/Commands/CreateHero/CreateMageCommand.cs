using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core.Contracts;
using Team8Project.Data;

namespace Team8Project.Core.Commands.CreateHero
{
    public class CreateMageCommand : Command, ICommand
    {
        public CreateMageCommand(IFactory factory, IDataContainer data) : base(factory, data)
        {
        }

        public override void Execute()
        {
            string name = "Penka";
            HeroClass heroClass = HeroClass.Mage;
            int healthPoints = 120;
            int dmgStartOfRange = 10;
            int dmgEndOfRange = 12;


            IHero hero = this.factory.CreateMage(name, heroClass, healthPoints, dmgStartOfRange, dmgEndOfRange);
            this.data.ListHeros.Add(hero);
        }
    }
}
