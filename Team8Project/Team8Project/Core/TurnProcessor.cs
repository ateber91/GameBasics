using Team8Project.Contracts;
using Team8Project.Core.Providers;

namespace Team8Project.Core
{
    public class TurnProcessor
    {
        private int turnNumeber = 1;
        private static TurnProcessor instance;
        private IHero activeHero;
        private IHero firstHero;
        private IHero secondHero;

        private TurnProcessor()
        {
        }
        public int TurnNumeber
        {
            get { return this.turnNumeber; }
            private set { this.turnNumeber = value; }
        }


        public void EndAct()
        {
            this.UpdateCooldowns();
            this.ActiveHero = ActiveHero.Opponent;
        }
        public void NextTurn()
        {
            this.turnNumeber++;
        }

        public void SetFirstTurnActiveHero()
        {
            SetRelationship();
            var res = RandomProvider.Generate(1, 2);
            if (res == 1)
            {
                this.ActiveHero = this.FirstHero;
            }
            else
            {
                this.ActiveHero = this.SecondHero;
            }
        }

        private void SetRelationship()
        {
            this.FirstHero.Opponent = this.SecondHero;
            this.SecondHero.Opponent = this.FirstHero;
        }

        public static TurnProcessor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TurnProcessor();
                }
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

        public IHero FirstHero
        {
            get { return firstHero; }
            set
            {
                firstHero = value;
            }
        }
        public IHero SecondHero
        {
            get { return secondHero; }
            set
            {
                secondHero = value;
            }
        }

        private void UpdateCooldowns()
        {
            ////refreshing cooldowns
            foreach (IAbility ability in this.ActiveHero.Abilities)
            {
                if (ability.OnCD == true)
                {
                    ability.CDCounter++;
                    if (ability.CDCounter == ability.Cd)
                    {
                        ability.OnCD = false;
                    }
                }
            }
        }
    }
}
