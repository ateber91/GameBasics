using Autofac;
using System.Linq;
using System.Reflection;
using Team8Project.Common.Providers;
using Team8Project.Contracts;

namespace Team8Project.Core.Advanced
{
    public class TerrainManager : ITerrainManager
    {
        private ITerrain terrain;
        private readonly IComponentContext context;

        public TerrainManager(IComponentContext context)
        {
            this.context = context;
        }

        public ITerrain Terrain
        {
            get { return this.terrain; }
            set { this.terrain = value; }
        }

        public void SetTerrain()
        {
            var terrainNames = Assembly.GetExecutingAssembly().DefinedTypes
                .Where(typeInfo => typeInfo.ImplementedInterfaces.Contains(typeof(ITerrain)) && typeInfo.IsAbstract == false)
                .Select(terrainType => terrainType.Name.ToLower())
                .ToList();

            int t = RandomProvider.Generate(0, terrainNames.Count - 1);

            this.Terrain = this.context.ResolveNamed<ITerrain>(terrainNames[t]);
        }

        public string ApplyContinuousEffect(IHero active)
        {
            int x = RandomProvider.Generate(1, 3);

            if (x == 1) { return this.Terrain.ContinuousEffect(active); }
            else if (x == 2) { return this.Terrain.ContinuousEffect(active.Opponent); }
            else { return "Terrain was merciful today"; }
        }
        public string ChangeDayNight()
        {
            this.Terrain.IsDay = (this.Terrain.IsDay) ? false : true;
            return (this.Terrain.IsDay) ? "Day has come" : "Night has come";
        }
    }
}
