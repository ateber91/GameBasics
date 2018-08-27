using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Common.Providers;
using Team8Project.Contracts;

namespace Team8Project.Models.Magic
{
    public class DamagingAbility : Ability, IDamagingAbility
    {
        private int damageDealt;

        public DamagingAbility(string name, int cd, HeroClass heroClass, EffectType type, int abilityPower)
            : base(name, cd, heroClass, type, abilityPower)
        {
        }

        public override void Apply()
        {
            if (Caster.Opponent.HasRessistance)
            {
                this.damageDealt = 0;
            }
            else
            {
                var heroDmg = RandomProvider.Generate(this.Caster.DmgStartOfRange, base.Caster.DmgEndOfRange); // calculate hero dmg
                this.damageDealt = heroDmg + base.AbilityPower; // adds ability power
            }
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
