using System;
using Team8Project.Common;
using Team8Project.Contracts;
using Team8Project.Common.Providers;
using Team8Project.Data;

namespace Team8Project.Core
{
    public class TurnProcessor
    {
        private int turnNumber = 1;
        private IHero activeHero;
        private IHero firstHero;
        private IHero secondHero;
        private readonly IDataContainer data;
        private readonly IFactory factory;

        public TurnProcessor(IDataContainer data, IFactory factory)
        {
            this.data = data;
            this.factory = factory;
        }

        public int TurnNumber
        {
            get { return this.turnNumber; }
            private set
            {
                Validations.ValidateRangeNumbers(value, Constants.MIN_TURN, Constants.MAX_TURN, $"Turns less than {Constants.MIN_TURN} or more than {Constants.MAX_TURN}");
                this.turnNumber = value;
            }
        }


      
        public void NextTurn()
        {
            this.turnNumber++;
        }

        public void SetFirstTurn()
        {
            SetRelationship();
            SetActiveHero();
            SetAbilities();
        }
        public void SetActiveHero()
        {
            var res = RandomProvider.Generate(1, 2);
            if (res == 1)
            {
                this.ActiveHero = this.firstHero;
            }
            else
            {
                this.ActiveHero = this.secondHero;
            }
        }
        private void SetAbilities()
        {
            factory.CreateSpellBook(this.ActiveHero);
            factory.CreateSpellBook(this.ActiveHero.Opponent);
        }
        private void SetRelationship()
        {
            this.firstHero = this.data.ListHeros[0];
            this.secondHero = this.data.ListHeros[1];

            this.firstHero.Opponent = this.secondHero;
            this.secondHero.Opponent = this.firstHero;


        }

        public IHero ActiveHero
        {
            get { return this.activeHero; }
            set
            {
                activeHero = value ?? throw new ArgumentNullException();
            }
        }

        public void UpdateCooldowns(IHero activeHero)
        {
            foreach (IAbility ability in activeHero.Abilities)
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
            foreach (IAbility ability in activeHero.Opponent.Abilities)
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
