using Team8Project.Contracts;

namespace Team8Project.Models
{
    public class Turn
    {
        public Turn()
        {
       //     NumberOfTurn++;
        }

        public int NumberOfTurn { get; set; }
        public IHero ActiveHero { get; set; }
        public IHero OpponentHero { get; set; }
        public string Log { get; set; }

    }
}
