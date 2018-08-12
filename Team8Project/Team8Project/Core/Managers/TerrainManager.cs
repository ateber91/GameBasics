using System;
using Team8Project.Contracts;
using Team8Project.Core.Providers;
using Team8Project.Models.Terrains;

namespace Team8Project.Core
{
    public class TerrainManager
    {
        private static TerrainManager instance;
        private ITerrain terrain;

        private TerrainManager()
        {
        }

        public ITerrain Terrain
        {
            get { return this.terrain; }
            set
            {
                this.terrain = value;
            }
        }

        public static TerrainManager Instance
        {
            get
            {
                if (instance == null) { instance = new TerrainManager(); }
                return instance;
            }
        }

        public void SetTerrain()
        {
            int t = RandomProvider.Generate(1, 3);

            switch (t)
            {
                case 1:
                    this.Terrain = Jungle.Instance;
                    break;
                case 2:
                    this.Terrain = Graveyard.Instance;
                    break;
                case 3:
                    this.Terrain = Tundra.Instance;
                    break;
                default:
                    break;
            }
        }

        public string ApplyInitialEffects(IHero activeHero)
        {
            return $"Initial terrain effect applied to {activeHero.Name} - {this.Terrain.HeroEffect(activeHero)}\nInitial terrain effect applied to {activeHero.Opponent.Name} - {this.Terrain.HeroEffect(activeHero.Opponent)}";
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
                return this.Terrain.ContinuousEffect(active.Opponent);            }
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
