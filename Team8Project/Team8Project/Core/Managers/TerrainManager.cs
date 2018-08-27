using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Team8Project.Contracts;
using Team8Project.Core.Providers;
using Team8Project.Models.Terrains;

namespace Team8Project.Core
{
    public class TerrainManager
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
            set
            {
                this.terrain = value;
            }
        }

        public void SetTerrain()
        {
            var terrainNames = Assembly.GetExecutingAssembly().DefinedTypes
                .Where(typeInfo =>
                    typeInfo.ImplementedInterfaces.Contains(typeof(ITerrain)) && typeInfo.IsAbstract == false)
                .Select(terrainType => terrainType.Name.ToLower())
                .ToList();

            int t = RandomProvider.Generate(0, terrainNames.Count - 1);

            this.Terrain = this.context.ResolveNamed<ITerrain>(terrainNames[t]);
        }

        public string ApplyContinuousEffect(IHero active)
        {
            int x = RandomProvider.Generate(1, 3);

            if (x == 1)
            {
                return this.Terrain.ContinuousEffect(active);
            }
            else if (x == 2)
            {
                return this.Terrain.ContinuousEffect(active.Opponent);
            }
            else
            {
                return "Terrain didn't affect any heros this turn.";
            }
        }

        public string ChangeDayNight()
        {
            this.Terrain.IsDay = (this.Terrain.IsDay) ? false : true;
            return (this.Terrain.IsDay) ? "Day has come" : "Night has come";
        }
    }
}
