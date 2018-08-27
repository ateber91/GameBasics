using System;
using System.Collections.Generic;
using System.Linq;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core.Providers;
using Team8Project.Models;
using Team8Project.Models.Characters;
using Team8Project.Models.Magic;
using Team8Project.Models.Magic.EffectAbilities;

namespace Team8Project.Core
{
    public class Factory : IFactory
    {
        public Factory()
        {
        }

        private IList<IAbility> PopulateSpellPool()
        {
            return new List<IAbility>()
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
                new Dot("Poison",2,HeroClass.Assasin,EffectType.DOT,2,8),
                new Buff("Critical",2,HeroClass.Assasin,EffectType.Buff,1,20),
                new Resistance("Evasion",2,HeroClass.Assasin,EffectType.Resistance,1,0),
                new Dot("Bleed",2,HeroClass.Warrior,EffectType.DOT,2,10),
                new Resistance("Block",2,HeroClass.Warrior,EffectType.Resistance,1,0),
                new Incapacitation("Stun",2,HeroClass.Warrior,EffectType.Incapacitated,1,0),
                new Resistance("Ice Barrier",2,HeroClass.Mage,EffectType.Resistance,1,0),
                new Dot("Burn",2,HeroClass.Mage,EffectType.DOT,2,9),
                new Incapacitation("Freeze",2,HeroClass.Mage,EffectType.Incapacitated,1,0),
                new Hot("Regeneration",2,HeroClass.Cleric,EffectType.HOT,2,15),
                new Debuff("Curse",2,HeroClass.Cleric,EffectType.Debuff,2,10),
                new Buff("Bless",2,HeroClass.Cleric,EffectType.Buff,2,20),
            };
        }
        public IHero CreateAssasin(string name, HeroClass heroClass, int healthPoints, int dmgStartOfRange, int dmgEndOfRange)
        {
            return new Assasin(name, heroClass, healthPoints, dmgStartOfRange, dmgEndOfRange);
        }

        public IHero CreateWarrior(string name, HeroClass heroClass, int healthPoints, int dmgStartOfRange, int dmgEndOfRange)
        {
            return new Warrior(name, heroClass, healthPoints, dmgStartOfRange, dmgEndOfRange);
        }
        public IHero CreateMage(string name, HeroClass heroClass, int healthPoints, int dmgStartOfRange, int dmgEndOfRange)
        {
            return new Mage(name, heroClass, healthPoints, dmgStartOfRange, dmgEndOfRange);
        }

        public IHero CreateCleric(string name, HeroClass heroClass, int healthPoints, int dmgStartOfRange, int dmgEndOfRange)
        {
            return new Cleric(name, heroClass, healthPoints, dmgStartOfRange, dmgEndOfRange);
        }



        public void CreateSpellBook(IHero hero)
        {
            var spellPool = PopulateSpellPool().Where(x => x.HeroClass == hero.HeroClass);
            var basicAttack = new DamagingAbility("Basic Attack", 0, hero.HeroClass, EffectType.Damage, 0);
            hero.Abilities.Add(basicAttack); //add basic attack

            var dmgAbilities = spellPool.Where(x => x.Type == EffectType.Damage).ToList();
            hero.Abilities.Add(dmgAbilities[RandomProvider.Generate(0, dmgAbilities.Count - 1)]);  //add 2nd spell                                                                                                      //?

            var effectAbilitues = spellPool.Where(x => x.Type != EffectType.Damage).ToList();
            hero.Abilities.Add(effectAbilitues[RandomProvider.Generate(0, effectAbilitues.Count - 1)]);   //add 3rd spell

            foreach (var ability in hero.Abilities) { ability.Caster = hero; }
        }
    }
}
