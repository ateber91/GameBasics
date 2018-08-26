using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Team8Project.Common;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core.Contracts;
using Team8Project.Data;
using Team8Project.IO;
using Team8Project.IO.Contracts;
using Team8Project.Models.Magic.EffectAbilities;

namespace Team8Project.Core
{

    public class GameEngine : IEngine
    {
        private readonly IFactory factory;
        private readonly TurnProcessor turn;
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IDataContainer data;
        private readonly TerrainManager terrainManager;
        private readonly CommandProcessor commandProcessor;
        private readonly IRender renderer;
        private readonly Checker checker;
        private bool endGame = false;

        public GameEngine(IFactory factory, TurnProcessor turn, IReader reader, IWriter writer,
            CommandProcessor commandProcessor, IDataContainer data, TerrainManager terrainManager,
            IRender render, Checker checker)
        {
            this.factory = factory;
            this.turn = turn;
            this.reader = reader;
            this.writer = writer;
            this.commandProcessor = commandProcessor;
            this.data = data;
            this.terrainManager = terrainManager;
            this.renderer = render;
            this.checker = checker;
        }

        public void Run()
        {
            PreBuildGame();

            //START GAME
            while (true)
            {
                this.renderer.UpdataScreen();
                //   this.data.Log.AppendLine($"Turn {this.turn.TurnNumber}: ");

                if (turn.TurnNumber % 3 == 0)
                {
                    this.data.Log.AppendLine(this.terrainManager.ChangeDayNight());
                }

                string continiousEffect = this.terrainManager.ApplyContinuousEffect(this.turn.ActiveHero);
                if (continiousEffect != string.Empty) { this.data.Log.AppendLine(continiousEffect); }


                this.renderer.UpdataScreen();


                Act(turn.ActiveHero); //first hero move
                turn.EndAct();
                //   this.renderer.UpdataScreen();
                if (this.endGame) { return; }
                Act(turn.ActiveHero); //second hero move
                turn.EndAct();
                //    this.renderer.UpdataScreen();

                turn.UpdateCooldowns(turn.ActiveHero);
                turn.NextTurn();
                // this.renderer.UpdataScreen();
            }
        }

        private void PreBuildGame()
        {
            renderer.SetScreenSize();
            renderer.InitialScreen();

            string[] players = new string[2];

            this.writer.ConsoleWrite("Player 1: ");
            players[0] = this.reader.ConsoleReadKey();
            this.writer.WriteLine("");
            this.writer.ConsoleWrite("Player 2: ");
            players[1] = this.reader.ConsoleReadKey();
            this.writer.ConsoleClear();

            commandProcessor.ProcessCommand(players);

            turn.SetFirstTurn();
            factory.CreateSpellBook(turn.ActiveHero);
            factory.CreateSpellBook(turn.ActiveHero.Opponent);

            this.terrainManager.SetTerrain();
            this.terrainManager.Terrain.ApplyInitialEffect(turn.ActiveHero);
        }

        private void Act(IHero activeHero)
        {
            if (this.checker.CheckForIncapacitation() == false)
            {
                foreach (var effect in turn.ActiveHero.AppliedEffects.ToList())
                {
                    var resultToBeLogged = effect.Affect();
                    if (resultToBeLogged != string.Empty) { this.data.Log.AppendLine(resultToBeLogged); }
                }

                this.renderer.UpdataScreen();
                this.renderer.UpdateActiveHero();

                var selectedAbilityCommand = this.checker.CheckIfabilityInputIsValid(this.reader.ConsoleReadKey());

                var selectedAbility = this.checker.CheckIfAbilityIsReadyForUse(this.commandProcessor.ProcessCommand(selectedAbilityCommand));

                turn.ActiveHero.UseAbility(selectedAbility);

                this.data.Log.AppendLine($"{turn.ActiveHero.Name} uses {selectedAbility.Name} and {selectedAbility.ToString()}.");
            }

            this.endGame = this.checker.CheckIfGameIsOver();
            this.renderer.UpdataScreen();
        }
    }
}