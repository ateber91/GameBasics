using Team8Project.Common;
using Team8Project.Contracts;
using Team8Project.Core;
﻿using System.Text;
using Team8Project.Core.Providers;
namespace Team8Project.Models.Magic
{
    public class DamagingAbility : Ability, IDamagingAbility
    {

        private int damageDealt;

        public DamagingAbility(string name, int cd, HeroClass heroClass, EffectType type, int abilityPower)
            : base(name, cd, heroClass, type, abilityPower)
        {
           // base.Target = base.Caster.Opponent;
        }

        //FIX : printing logic
        public override void Apply()
        { //TEST this:
            var heroDmg = RandomProvider.Generate(this.Caster.DmgStartOfRange, base.Caster.DmgEndOfRange);
            this.damageDealt = heroDmg + base.AbilityPower;
          
            damageDealt = EffectManager.Instance.TransformDamage(damageDealt, this.Caster);
            base.Caster.Opponent.HealthPoints -= damageDealt;
            this.CD2 = -1;
            this.OnCD = true;
        }

        public override string ToString()
        {
            return $"deals {this.damageDealt} damage";
        }

        public override string Print()
        {
            //string Name { get; set; }
            //int Cd { get; set; }
            //IHero Caster { get; set; }

            var sb = new StringBuilder();

            sb.AppendLine("Spell name: " + this.Name);
            sb.AppendLine("Spell cooldown " + this.Cd);

            return sb.ToString();
        }
    }
}
