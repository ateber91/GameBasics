using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Common;
using Team8Project.Contracts;

namespace Team8Project.Core
{
    public class CommandProcessor
    {
        private List<IHero> listHeros;
        private Factory factory;
        private TurnProcessor turn;
        public CommandProcessor()
        {
            this.factory = Factory.Instance;
            this.turn = TurnProcessor.Instance;
            this.listHeros = new List<IHero>();
        }
        public List<IHero> ListHeros
        {
            get
            {
                return new List<IHero>(this.listHeros);
            }
            
        }

        public List<IHero> ProcessCommand(string[] players)
        {
            foreach (var player in players)
            {
                switch (player)
                {
                    case "2":
                        this.listHeros.Add(this.factory.CreateHero(HeroClass.Warrior));
                        break;
                    case "3":
                        this.listHeros.Add(this.factory.CreateHero(HeroClass.Mage));
                        break;
                    case "1":
                        this.listHeros.Add(this.factory.CreateHero(HeroClass.Assasin));
                        break;
                    case "4":
                        this.listHeros.Add(this.factory.CreateHero(HeroClass.Cleric));
                        break;
                    default:
                        throw new ArgumentException("I couldn't create you hero! :(");
                }
            }
            return this.ListHeros;
        }

        public IAbility ProcessCommand(string key)
        {
            switch (key)
            {
                case "1":
                    return turn.ActiveHero.Abilities[0];
                case "2":
                    return turn.ActiveHero.Abilities[1];
                case "3":
                    return turn.ActiveHero.Abilities[2];
                default:
                    throw new ArgumentException("I couldn't return ability! :(");
            }
        }
    }
}
