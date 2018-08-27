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
        private readonly TerrainManager terrainManager;
        private readonly CommandProcessor commandProcessor;
        private readonly IRenderer renderer;
        private readonly IActManager actManager;

        public GameEngine(TurnProcessor turn, CommandProcessor commandProcessor,
                          IDataContainer data, TerrainManager terrainManager,
                          IRenderer render, IActManager actManager)
        {
            this.turn = turn;
            this.commandProcessor = commandProcessor;
            this.data = data;
            this.terrainManager = terrainManager;
            this.renderer = render;
            this.actManager = actManager;
        }

        public void Run()
        {
            PreBuildGame();
            while (true)
            {
                this.renderer.UpdataScreen();
                this.data.Log.AppendLine($"Turn: {turn.TurnNumber}:");

                if (turn.TurnNumber % 3 == 0)
                {
                    this.data.Log.AppendLine(this.terrainManager.ChangeDayNight());
                }

                string continiousEffect = this.terrainManager.ApplyContinuousEffect(this.turn.ActiveHero);
                if (continiousEffect != string.Empty) { this.data.Log.AppendLine(continiousEffect); }

                this.renderer.UpdataScreen();

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
            this.terrainManager.Terrain.ApplyInitialEffect(turn.ActiveHero);
        }
    }
}