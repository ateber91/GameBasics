using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Core
{
    public interface IFactory
    {
        void CreateSpellBook(IHero hero);
        IHero CreateAssasin(string name, HeroClass heroClass, int healthPoints, int dmgStartOfRange, int dmgEndOfRange);
        IHero CreateWarrior(string name, HeroClass heroClass, int healthPoints, int dmgStartOfRange, int dmgEndOfRange);
        IHero CreateCleric(string name, HeroClass heroClass, int healthPoints, int dmgStartOfRange, int dmgEndOfRange);
        IHero CreateMage(string name, HeroClass heroClass, int healthPoints, int dmgStartOfRange, int dmgEndOfRange);

    }
}