﻿using System;
using System.Collections.Generic;
using System.Linq;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core.Providers;
using Team8Project.Models;
using Team8Project.Models.Magic;

namespace Team8Project.Core
{
    public class Factory
    {
        private static Factory instance;
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
            get
            {
                if (instance == null) { instance = new Factory(); }
                return instance;
            }
        }

        public IHero CreateHero(HeroClass heroClass)
        {
            IHero hero;
            switch (heroClass)
            {
                case HeroClass.Warrior: hero = this.SetStats("Pesho", 200, 12, 18); break;
                case HeroClass.Mage: hero = this.SetStats("Penka", 200, 10, 12); break;
                case HeroClass.Assasin: hero = this.SetStats("Gesho", 200, 15, 20); break;
                case HeroClass.Cleric: hero = this.SetStats("Genka", 200, 8, 10); break;
                default: throw new ArgumentException("Invalid hero class");
            }
            hero.HeroClass = heroClass;
            return hero;
        }

        public void CreateSpellBook(IHero hero)
        {
            var basicAttack = new DamagingAbility("Basic Attack", 0, hero.HeroClass, EffectType.Damage, 0);
            hero.Abilities.Add(basicAttack); //add basic attack

            var dmgAbilities = this.spellPool.Where(x => x.HeroClass == hero.HeroClass && x.Type == EffectType.Damage).ToList();
            hero.Abilities.Add(dmgAbilities[RandomProvider.Generate(0, dmgAbilities.Count - 1)]);  //add 2nd spell                                                                                                      //?

            var effectAbilitues = spellPool.Where(x => x.HeroClass == hero.HeroClass && x.Type != EffectType.Damage).ToList();
            hero.Abilities.Add(effectAbilitues[RandomProvider.Generate(0, effectAbilitues.Count - 1)]);   //add 3rd spell

            foreach (var ability in hero.Abilities) { ability.Caster = hero; }
        }

        private IHero SetStats(string name, int hp, int dmgStartOfRange, int dmgEndOfRange)
        {
            return new Hero(name, hp, dmgStartOfRange, dmgEndOfRange);
        }

    }
}
