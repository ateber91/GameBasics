using System;
using System.Collections.Generic;
using System.Linq;
using Team8Project.Common;
using Team8Project.Contracts;
using Team8Project.Core.Providers;
using Team8Project.Models;
using Team8Project.Models.Magic;
using Team8Project.Models.Statuses;
using Team8Project.Models.Terrains;

namespace Team8Project.Core
{
    public class Factory
    {
        private static Factory instance = new Factory();
        private IList<IAbility> spellPool;
        private Factory()
        {
            PopulateSpellPool();
        }

        private void PopulateSpellPool()
        {
            this.spellPool = new List<IAbility>()
            {
                new DamagingAbility("Heroic Attack",1, HeroClass.Warrior,EffectType.Damage,15),
                new DamagingAbility("Blade Storm", 1,HeroClass.Warrior,EffectType.Damage, 18),
                new DamagingAbility("Mortal Strike", 1,HeroClass.Warrior,EffectType.Damage, 17),
                new DamagingAbility("Fire Ball", 1, HeroClass.Mage,EffectType.Damage, 25),
                new DamagingAbility("Arcane Missles", 1, HeroClass.Mage,EffectType.Damage, 22),
                new DamagingAbility("Flame Strike", 1, HeroClass.Mage,EffectType.Damage, 20),
                new DamagingAbility("Backstab",1,  HeroClass.Assasin,EffectType.Damage,20),
                new DamagingAbility("Sinister Strike", 1, HeroClass.Assasin,EffectType.Damage, 18),
                new DamagingAbility("Eviscerate", 1,  HeroClass.Assasin,EffectType.Damage,22),
                new DamagingAbility("Smite",1, HeroClass.Cleric,EffectType.Damage,15),
                new DamagingAbility("Holy Fire", 1, HeroClass.Cleric,EffectType.Damage, 18),
                new DamagingAbility("Penance", 1, HeroClass.Cleric,EffectType.Damage, 17),
                new Effect("Poison",2,HeroClass.Assasin,EffectType.DOT,2,8),
                new Effect("Critical",2,HeroClass.Assasin,EffectType.Buff,1,20),
                new Effect("Evasion",2,HeroClass.Assasin,EffectType.Resistance,1,0),
                new Effect("Bleed",2,HeroClass.Warrior,EffectType.DOT,2,10),
                new Effect("Block",2,HeroClass.Warrior,EffectType.Resistance,1,0),
                new Effect("Stun",2,HeroClass.Warrior,EffectType.Incapacitated,1,0),
                new Effect("Ice Barrier",2,HeroClass.Mage,EffectType.Resistance,1,0),
                new Effect("Burn",2,HeroClass.Mage,EffectType.DOT,2,9),
                new Effect("Freeze",2,HeroClass.Mage,EffectType.Incapacitated,1,0),
                new Effect("Regeneration",2,HeroClass.Cleric,EffectType.HOT,2,15),
                new Effect("Curse",2,HeroClass.Cleric,EffectType.Debuff,2,20),
                new Effect("Bless",2,HeroClass.Cleric,EffectType.Buff,2,20),

            };
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
            return hero;
        }

        public void CreateSpellBook(IHero hero)
        {
            hero.Abilities = Spellbook(hero.HeroClass);
            foreach (var ability in hero.Abilities)
            {
                ability.Caster = hero;
                if (ability.Type == EffectType.Buff || ability.Type == EffectType.HOT || ability.Type == EffectType.Resistance)
                {
                    ability.Target = ability.Caster;
                }
                else if (ability.Type == EffectType.Damage || ability.Type == EffectType.Debuff || ability.Type == EffectType.DOT || ability.Type == EffectType.Incapacitated)
                {
                    ability.Target = ability.Caster.Opponent;
                }
            }
        }


        private IList<IAbility> Spellbook(HeroClass heroClass)
        {
            var spellBook = new List<IAbility>();
            spellBook.Add(new DamagingAbility("Basic Attack", 0, heroClass, EffectType.Damage, 0)); //add basic attack

            var dmgAbilities = this.spellPool.Where(x => x.HeroClass == heroClass && x.Type == EffectType.Damage).ToList(); //add 2nd spell
            spellBook.Add(dmgAbilities[RandomProvider.Generate(0, dmgAbilities.Count - 1)]);                                                                                                       //?

            var effectAbilitues = spellPool.Where(x => x.HeroClass == heroClass && x.Type != EffectType.Damage).ToList(); //add 3rd spell
            spellBook.Add(effectAbilitues[RandomProvider.Generate(0, effectAbilitues.Count - 1)]);

            return spellBook;
        }

    }
}
