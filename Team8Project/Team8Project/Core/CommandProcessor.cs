using System;
using System.Collections.Generic;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Data;

namespace Team8Project.Core
{
    public class CommandProcessor
    {
        private IFactory factory;
        private TurnProcessor turn;
        private readonly IDataContainer data;

        public CommandProcessor(IFactory factory, TurnProcessor turn, IDataContainer data)
        {
            this.factory = factory;
            this.turn = turn;
            this.data = data;
        }
   

        public void ProcessCommand(string[] players)
        {
            foreach (var player in players)
            {
                switch (player)
                {
                    case "1": this.data.ListHeros.Add(this.factory.CreateHero(HeroClass.Assasin)); break;
                    case "2": this.data.ListHeros.Add(this.factory.CreateHero(HeroClass.Warrior)); break;
                    case "3": this.data.ListHeros.Add(this.factory.CreateHero(HeroClass.Mage)); break;
                    case "4": this.data.ListHeros.Add(this.factory.CreateHero(HeroClass.Cleric)); break;
                    default: throw new ArgumentException("I couldn't create you hero! :(");
                }
            }
           
        }

        public IAbility ProcessCommand(string key)
        {
            switch (key)
            {
                case "1": return turn.ActiveHero.Abilities[0];
                case "2": return turn.ActiveHero.Abilities[1];
                case "3": return turn.ActiveHero.Abilities[2];
                default: throw new ArgumentException("I couldn't select ability! :(");
            }
        }
    }
}
