using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            get
            {
                return this.terrain;
            }
            set
            {
                this.terrain = value;
            }
        }

        public static TerrainManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TerrainManager();
                }
                return instance;
            }
        }

        public void SetTerrain()
        {
            int x = RandomProvider.Generate(1, 3);
            switch (x)
            {
                case 1: this.Terrain = Jungle.Instance;
                    Console.WriteLine("Jungle set as terrain");
                    break;
                case 2: this.Terrain = Graveyard.Instance;
                    Console.WriteLine("Graveyard set as terrain");
                    break;
                case 3: this.Terrain = Tundra.Instance;
                    Console.WriteLine("Tundra set as terrain");
                    break;
                default:
                    break;
            }
        }

        public void ApplyInitialEffects(IHero activeHero)
        {
            this.Terrain.HeroEffect(activeHero);
            this.Terrain.HeroEffect(activeHero.Opponent);
            Console.WriteLine("Initial terrain effects applied to both heroes");
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
    }
}
