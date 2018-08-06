using System.Collections.Generic;
using Team8Project.Common;
using Team8Project.Contracts;
using Team8Project.Models;

namespace Team8Project.Core.Providers
{
    public class HeroPool
    {
        private static readonly HeroPool instance = new HeroPool();

        private HeroPool()
        {
            this.Heroes = new List<IHero>()
            {
                 new Hero("Pesho",200,15,20,HeroClass.Assasin),
                 new Hero ("Gesho",220,12,18,HeroClass.Warrior),
                 new Hero ("Stoyan",220,12,18,HeroClass.Warrior),
                 new Hero("Penka",180,10,12,HeroClass.Mage),
                 new Hero("Petka",160,8,10,HeroClass.Cleric)
                };
        }

        public ICollection<IHero> Heroes { get; private set; }

        public static HeroPool Instance
        {
            get
            {
                return instance;
            }
        }
    }


    class HeroFactory
    {

        public IHero CreateHero(string name, HeroClass heroClass)
        {
            switch (heroClass)
            {
                case HeroClass.Warrior:
                    return new Hero(name, 220, 12, 18, HeroClass.Warrior);
                case HeroClass.Mage:
                    break;
                case HeroClass.Assasin:
                    break;
                case HeroClass.Cleric:
                    break;
                default:
                    break;
            }

            return null;
        }
    }

}

