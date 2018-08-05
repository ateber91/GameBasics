using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Common;
using Team8Project.Contracts;
using Team8Project.Models;

namespace Team8Project.Core
{
    public class Factory
    {
        private static Factory instance = new Factory();

        private Factory() { }

        public static Factory Instance
        {
            get
            {
                return instance;
            }
        }

        public IHero CreateHero(string name, int healthPoints, int dmgStartOfRange, int dmgEndOfRange, HeroClass heroClass)
        {
            return new Hero(name,healthPoints,dmgStartOfRange,dmgEndOfRange,heroClass);
        }




    }
}
