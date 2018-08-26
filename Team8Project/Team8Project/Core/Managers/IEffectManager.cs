using Team8Project.Contracts;

namespace Team8Project.Core
{
    public interface IEffectManager
    {
        void AtTurnStart(IHero activeHero);
        void RemoveExpired(IHero activeHero);
        int TransformDamage(int damage, IHero activeHero);
    }
}