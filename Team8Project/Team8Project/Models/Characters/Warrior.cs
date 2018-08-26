using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Common.Enums;
using Team8Project.Contracts;

namespace Team8Project.Models.Characters
{
    public class Warrior : Hero, IHero
    {
        public Warrior(string name, HeroClass heroClass, int healthPoints, int dmgStartOfRange, int dmgEndOfRange) : base(name, heroClass, healthPoints, dmgStartOfRange, dmgEndOfRange)
        {
        }

        public override void InitializeJungle()
        {
            this.HealthPoints += 50;
        }

        public override void InitializeGraveyard()
        {
            foreach (var ability in this.Abilities.OfType<IDamagingAbility>())
            {
                ability.AbilityPower -= 10;
            }
            //return $"{hero.Name}'s damaging abilities decreased by 10";
        }

        public override void InitializeTundra()
        {
            this.HealthPoints -= 25;
            //return $"{hero.Name}'s health points decreased by 25";
        }
    }
}