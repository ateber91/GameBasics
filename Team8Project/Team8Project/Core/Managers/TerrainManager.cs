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
            GameEngine.Instance.Log.AppendLine("Initial terrain effect applied to " + activeHero.Name);
            this.Terrain.HeroEffect(activeHero);

            GameEngine.Instance.Log.AppendLine("Initial terrain effect applied to " + activeHero.Opponent.Name);
            this.Terrain.HeroEffect(activeHero.Opponent);
        }

        public void ApplyContinuousEffect(IHero active, IHero opponent)
        {
            int x = RandomProvider.Generate(1, 3);
            if (x == 1)
            {
                this.Terrain.ContinuousEffect(active);
            }
            else if (x == 2)
            {
                this.Terrain.ContinuousEffect(opponent);            }
            else
            {
                GameEngine.Instance.Log.AppendLine("Terrain was merciful");
            }
        }

        public void ChangeDayNight()
        {
            this.Terrain.IsDay = (this.Terrain.IsDay) ? false : true;
            Console.WriteLine((this.Terrain.IsDay) ? "Day has come" : "Night has come");
        }
    }
}
