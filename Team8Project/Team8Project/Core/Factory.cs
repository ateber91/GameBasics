using System;
using System.Collections.Generic;
using System.Linq;
using Team8Project.Common;
using Team8Project.Contracts;
using Team8Project.Core.Providers;
using Team8Project.Models;
using Team8Project.Models.Magic;

namespace Team8Project.Core
{
    public class Factory
    {
        private static Factory instance = new Factory();

        private Factory()
        {
        }

        public static Factory Instance
        {
            get { return instance; }
        }

        public IHero CreateHero(HeroClass heroClass)
        {
            IHero hero = new Hero();
            switch (heroClass)
            {
                case HeroClass.Warrior:
                    hero.Name = "Pesho";
                    hero.HealthPoints = 220;
                    hero.DmgStartOfRange = 12;
                    hero.DmgEndOfRange = 18;
                    break;
                // return new Hero("Pesho", 220, 12, 18, heroClass, Spellbook(heroClass));
                case HeroClass.Mage:
                    hero.Name = "Penka";
                    hero.HealthPoints = 180;
                    hero.DmgStartOfRange = 10;
                    hero.DmgEndOfRange = 12;
                    break;
                //  return new Hero("Penka", 180, 10, 12, heroClass, Spellbook(heroClass));
                case HeroClass.Assasin:
                    hero.Name = "Gesho";
                    hero.HealthPoints = 200;
                    hero.DmgStartOfRange = 15;
                    hero.DmgEndOfRange = 20;
                    break;
                //  return new Hero("Gesho", 200, 15, 20, heroClass, Spellbook(heroClass));
                case HeroClass.Cleric:
                    hero.Name = "Pesho";
                    hero.HealthPoints = 160;
                    hero.DmgStartOfRange = 8;
                    hero.DmgEndOfRange = 10;
                    break;
                //   return new Hero("Genka", 160, 8, 10, heroClass, Spellbook(heroClass));
                default:
                    throw new ArgumentException("Invalid hero class");
            }
            hero.HeroClass = heroClass;
            hero.Abilities = Spellbook(heroClass);
            foreach (var ability in hero.Abilities) { ability.Caster = hero; }
            return hero;
        }




        private IList<IAbility> Spellbook(HeroClass heroClass)
        {
            var spellBook = new List<IAbility>();
            spellBook.Add(new DamagingAbility("Basic Attack", 0, 0));
            switch (heroClass)
            {
                case HeroClass.Warrior:
                    var warriorSpellBook = new List<IAbility>()
            {
                new DamagingAbility("Heroic Attack",1,15),
                new DamagingAbility("Blade Storm", 1, 18),
                new DamagingAbility("Mortal Strike", 1, 17),

                //Todo: add 3rd effect ability
            };
                    spellBook.Add(warriorSpellBook[RandomProvider.Generate(0, warriorSpellBook.Count())]);
                    break;
                case HeroClass.Mage:

                    var mageSpellBook = new List<IAbility>()
            {
                new DamagingAbility("Fire Ball", 1, 25),
                new DamagingAbility("Arcane Missles", 1, 22),
                new DamagingAbility("Flame Strike", 1, 20),
                                //Todo: add 3rd effect ability

            };
                    spellBook.Add(mageSpellBook[RandomProvider.Generate(0, mageSpellBook.Count())]);

                    break;
                case HeroClass.Assasin:
                    var assasinSpellBook = new List<IAbility>()
            {
                new DamagingAbility("Mutilate",1,20),
                new DamagingAbility("Sinister Strike", 1, 18),
                new DamagingAbility("Eviscerate", 1, 22),
                                //Todo: add 3rd effect ability
            };
                    spellBook.Add(assasinSpellBook[RandomProvider.Generate(0, assasinSpellBook.Count())]);

                    break;
                case HeroClass.Cleric:
                    var clericSpellBook = new List<IAbility>()
            {
                new DamagingAbility("Smite",1,15),
                new DamagingAbility("Holy Fire", 1, 18),
                new DamagingAbility("Penance", 1, 17),
                                //Todo: add 3rd effect ability

            };
                    spellBook.Add(clericSpellBook[RandomProvider.Generate(0, clericSpellBook.Count())]);

                    break;
                default: throw new ArgumentException("Invalid hero class");
            }

            return spellBook;
        }
    }
}
