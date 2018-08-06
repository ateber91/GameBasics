using System.Collections.Generic;
using System.Linq;
using Team8Project.Common;
using Team8Project.Contracts;
using Team8Project.Core.Providers;
using Team8Project.Models.Magic;

namespace Team8Project.Core
{
    public class Factory
    {
        private static Factory instance = new Factory();
        private readonly HeroPool heroPool;

        private Factory()
        {
            this.heroPool = HeroPool.Instance;
        }

        public static Factory Instance
        {
            get
            {
                return instance;
            }
        }

        public IHero CreateHero(HeroClass heroClass)
        {
            var hero = heroPool.Heroes.FirstOrDefault(x => x.HeroClass == heroClass);
            hero.Abilities = AddSpellbook(heroClass);
            return hero;
        }

        private IList<IAbility> AddSpellbook(HeroClass heroClass)
        {
            //TODO: GET SPELLS FROM SPELLPOOL
            //todo: FIX which hero gets what spellbook
            var spellBook = new List<IAbility>();
            if (heroClass == HeroClass.Warrior)
            {
                DamagingAbility bladestorm = new DamagingAbility("bladestorm", 2, 50);
                spellBook.Add(bladestorm);
            }
            else if (heroClass == HeroClass.Assasin)
            {
                DamagingAbility sinisterStrike = new DamagingAbility("sinister strike", 2, 40);
                spellBook.Add(sinisterStrike);
            }
            return spellBook;

        }



    }
}
