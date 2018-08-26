using Team8Project.Common;
using Team8Project.Contracts;
using Team8Project.Core;
using System.Text;
using Team8Project.Core.Providers;
using Team8Project.Common.Enums;

namespace Team8Project.Models.Magic
{
    public class DamagingAbility : Ability, IDamagingAbility
    {
        private int damageDealt;
       private EffectManager effectManager;

        public DamagingAbility(string name, int cd, HeroClass heroClass, EffectType type, int abilityPower)
            : base(name, cd, heroClass, type, abilityPower)
        {
            
        }

        public override void Apply()
        {
            var heroDmg = RandomProvider.Generate(this.Caster.DmgStartOfRange, base.Caster.DmgEndOfRange); // calculate hero dmg
            this.damageDealt = heroDmg + base.AbilityPower; // adds ability power

            damageDealt = this.effectManager.TransformDamage(damageDealt, this.Caster); //transfroms dmg based on effects on self and opponent
            base.Caster.Opponent.HealthPoints -= damageDealt; //deals dmg to hp
            base.Apply(); //cd applied
        }

        public override string ToString()
        {
            return $"deals {this.damageDealt} damage";
        }

        public override string Print()
        {
            var sb = new StringBuilder();
            sb.Append($"{this.Name} {this.Caster.DmgStartOfRange + this.AbilityPower} - {this.Caster.DmgEndOfRange + this.AbilityPower} dmg");
            sb.Append(base.Print());
            return sb.ToString();
        }
    }
}
