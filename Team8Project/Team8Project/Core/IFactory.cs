using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Core
{
    public interface IFactory
    {
        IHero CreateHero(HeroClass heroClass);
        void CreateSpellBook(IHero hero);
    }
}