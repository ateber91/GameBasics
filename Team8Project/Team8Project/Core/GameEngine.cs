using Team8Project.Core.Advanced;
using Team8Project.Core.Commands;
using Team8Project.Core.Contracts;
using Team8Project.Data;
using Team8Project.IO.Contracts;

namespace Team8Project.Core
{
    public class GameEngine : IEngine
    {
        private readonly ITurnProcessor turn;
        private readonly IDataContainer data;
        private readonly IAdvancedChecker checker;
        private readonly ITerrainManager terrainManager;
        private readonly ICommandProcessor commandProcessor;
        private readonly IRenderer renderer;
        private readonly IActManager actManager;

        public GameEngine(ITurnProcessor turn, ICommandProcessor commandProcessor, IDataContainer data,
            ITerrainManager terrainManager, IRenderer render, IActManager actManager, IAdvancedChecker checker)
        {
            this.commandProcessor = commandProcessor;
            this.turn = turn;
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
            this.turn.ActiveHero.InitializeTerrain(this.terrainManager.Terrain);
            this.turn.ActiveHero.Opponent.InitializeTerrain(this.terrainManager.Terrain);
        }
    }
}