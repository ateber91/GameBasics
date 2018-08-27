using Team8Project.Contracts;

namespace Team8Project.Core.Advanced
{
    public interface ITerrainManager
    {
        ITerrain Terrain { get; set; }

        string ApplyContinuousEffect(IHero active);
        string ChangeDayNight();
        void SetTerrain();
    }
}