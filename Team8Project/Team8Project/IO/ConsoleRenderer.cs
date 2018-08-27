using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Common;
using Team8Project.Common.Enums;
using Team8Project.Core;
using Team8Project.Data;
using Team8Project.IO.Contracts;

namespace Team8Project.IO
{
    public class ConsoleRenderer : IRenderer
    {
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IDataContainer data;
        private readonly TerrainManager terrainManager;
        private readonly TurnProcessor turn;
        public ConsoleRenderer(IWriter writer, IReader reader, IDataContainer data, TerrainManager terrainManager, TurnProcessor turn)
        {
            this.writer = writer;
            this.reader = reader;
            this.data = data;
            this.terrainManager = terrainManager;
            this.turn = turn;
        }
        public void UpdataScreen()
        {
            this.writer.ConsoleClear();
            this.writer.PrintOnPosition(Constants.LOG_ROW_POS - 1, Constants.LOG_COL_POS, new String('-', Console.WindowWidth));
            this.writer.PrintOnPosition(Constants.LOG_ROW_POS, Constants.LOG_COL_POS, this.data.Log.ToString());

            this.writer.PrintOnPosition(0, 0, $"{this.terrainManager.Terrain.GetType().Name} set as terrain");
            this.writer.PrintOnPosition(0, 150, $" Turn: {turn.TurnNumber}", ConsoleColor.Red);
            this.writer.WriteLine(new String('-', Console.WindowWidth));
        }

        public void InitialScreen()
        {
            this.writer.WriteLine(string.Format(Constants.INITIAL_MESSAGE, HeroClass.Assasin, HeroClass.Warrior, HeroClass.Mage, HeroClass.Cleric));
            this.writer.WriteLine(new String('-', Console.WindowWidth));
        }

        public void UpdateActiveHero()
        {
            writer.WriteLine($"{turn.ActiveHero.HeroClass.ToString()} { turn.ActiveHero.Name} is active. HP: {turn.ActiveHero.HealthPoints}");
            this.writer.WriteLine($"{turn.ActiveHero.Name}'s abilities: ");

            int pos = 0;
            foreach (var ability in turn.ActiveHero.Abilities)
            {
                pos++;
                writer.WriteLine($"{pos}. {ability.Print()}");
            }
            if (turn.ActiveHero.AppliedEffects.Count == 0) { this.writer.WriteLine("Applied effects: No effects."); }
            else { this.writer.WriteLine($"Applied effects: {string.Join(", ", turn.ActiveHero.AppliedEffects)}"); }
        }

        public void SetScreenSize()
        {
            Console.SetWindowSize(160, 40);
        }

        public string[] CharacterSelection()
        {
            string[] players = new string[2];
            this.writer.ConsoleWrite("Player 1: ");
            players[0] = this.reader.ConsoleReadKey();
            this.writer.WriteLine("");
            this.writer.ConsoleWrite("Player 2: ");
            players[1] = this.reader.ConsoleReadKey();
            this.writer.ConsoleClear();
            return players;
        }
    }
}
