using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Contracts;
using Team8Project.Core.Providers;

namespace Team8Project.Models
{
    public class Turn
    {
        private int turnNumeber = 1;
        private static readonly Turn instance = new Turn();
        private IHero activeHero;


        private Turn()
        {
        }
        public int TurnNumeber
        {
            get { return this.turnNumeber; }
            private set { this.turnNumeber = value; }
        }


        public static void CheckActiveHeroStatus()
        {
            //foreach (var status in collection)
            //{

            //}
        }


        public IHero SwapTurnHolder()
        {
            //this.ActiveHero.HasTurn = false;
            //this.ActiveHero.Opponent.HasTurn = true;  
            this.TurnNumeber++;

            return this.ActiveHero.Opponent;
        }

        public IHero SetWhoIsFirst()
        {
            var res = RandomProvider.Generate(1, 3);
            if (res == 1)
            {
                return this.activeHero;
            }
                return this.activeHero.Opponent;
        }

        public static Turn Instance
        {
            get
            {
                return instance;
            }
        }

        public IHero ActiveHero
        {
            get { return this.activeHero; }
            set
            {
                activeHero = value;
            }
        }

    }
}
