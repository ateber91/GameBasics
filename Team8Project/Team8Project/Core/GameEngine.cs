using Team8Project.Core.Contracts;
using Team8Project.Core.Managers;
using Team8Project.Data;
using Team8Project.IO.Contracts;

namespace Team8Project.Core
{

    public class GameEngine : IEngine
    {
        private readonly TurnProcessor turn;
        private readonly IDataContainer data;
        private readonly AdvancedChecker checker;
        private readonly TerrainManager terrainManager;
        private readonly CommandProcessor commandProcessor;
        private readonly IRenderer renderer;
        private readonly IActManager actManager;

        public GameEngine(TurnProcessor turn, CommandProcessor commandProcessor,
                          IDataContainer data, TerrainManager terrainManager,
                          IRenderer render, IActManager actManager, AdvancedChecker checker)
        {
            this.turn = turn;
            this.commandProcessor = commandProcessor;
            this.data = data;
            this.terrainManager = terrainManager;
            this.renderer = render;
            this.actManager = actManager;
            this.checker = checker;
        }

        public void Run()
        {
            PreBuildGame();
            while (true)
            {
                this.renderer.UpdataScreen();
                this.data.Log.AppendLine($"Turn: {turn.TurnNumber}:");

                this.checker.CheckForTerrainContitiousEffect();

               

                //first hero move
                this.actManager.Act(turn.ActiveHero);
                if (this.data.EndGame) { return; }
                //second hero move
                this.actManager.Act(turn.ActiveHero);

                this.turn.UpdateCooldowns(turn.ActiveHero);
                this.turn.NextTurn();
            }
        }

        private void PreBuildGame()
        {
            this.renderer.SetScreenSize();
            this.renderer.InitialScreen();
            this.commandProcessor.ProcessCommand(this.renderer.CharacterSelection());
            this.turn.SetFirstTurn();
            this.terrainManager.SetTerrain();
            turn.ActiveHero.InitializeTerrain(this.terrainManager.Terrain);
            turn.ActiveHero.Opponent.InitializeTerrain(this.terrainManager.Terrain);
        }
    }
}