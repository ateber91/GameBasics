using System;
using Team8Project.Contracts;
using Team8Project.Core.Providers;
using Team8Project.Models.Terrains;

namespace Team8Project.Core
{
    public class TerrainManager
    {
        private static TerrainManager instance;
        private string tarrainType;
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

        public string TarrainType
        {
            get { return this.tarrainType; }
            private set
            {
                this.tarrainType = value;
            }
        }

        public void SetTerrain()
        {
            Random random = new Random();

            int x = random.Next(1, 4);
            switch (x)
            {
                case 1:
                    this.Terrain = Jungle.Instance;
                    this.TarrainType = "Jungle set as terrain";
                    break;
                case 2:
                    this.Terrain = Graveyard.Instance;
                    this.TarrainType = "Graveyard set as terrain";
                    break;
                case 3:
                    this.Terrain = Tundra.Instance;
                    this.TarrainType = "Tundra set as terrain";
                    break;
                default:
                    break;
            }
        }

        public void ApplyInitialEffects(IHero activeHero)
        {
            this.Terrain.HeroEffect(activeHero);
            this.Terrain.HeroEffect(activeHero.Opponent);
        }

        public void ApplyContinuousEffect(IHero activeHero)
        {
            if (RandomProvider.Generate(1, 2) == 1)
            {
                this.Terrain.ContinuousEffect(activeHero);
                Console.WriteLine(activeHero.Name + this.Terrain.ToString());
            }
            else
            {
                this.Terrain.ContinuousEffect(activeHero.Opponent);
                Console.WriteLine(activeHero.Opponent.Name + this.Terrain.ToString());
            }
        }

        public void ChangeDayNight()
        {
            this.Terrain.IsDay = (this.Terrain.IsDay) ? false : true;
            Console.WriteLine((this.Terrain.IsDay) ? "Day has come" : "Night has come");
        }
    }
}
