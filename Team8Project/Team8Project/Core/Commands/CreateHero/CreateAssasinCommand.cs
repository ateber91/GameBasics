using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core.Contracts;
using Team8Project.Data;

namespace Team8Project.Core.Commands.CreateHero
{
    public class CreateAssasinCommand : Command, ICommand
    {
        public CreateAssasinCommand(IFactory factory, IDataContainer data)
            : base(factory, data)
        {
        }

        public override void Execute()
        {
            string name = "Pesho";
            HeroClass heroClass = HeroClass.Assasin;
            int healthPoints = 150;
            int dmgStartOfRange = 12;
            int dmgEndOfRange = 18;

            IHero hero = this.factory.CreateAssasin(name, heroClass, healthPoints, dmgStartOfRange, dmgEndOfRange);
            this.data.ListHeros.Add(hero);
        }
    }
}
