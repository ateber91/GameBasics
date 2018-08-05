using Team8Project.Contracts;
using Team8Project.Core.Providers;
namespace Team8Project.Models.Magic
{
    public class DamagingAbility : IDamagingAbility
    {
        private int spellPower;
        private string name;
        private int cd;
        private IHero caster;
        private string result;

        public DamagingAbility(string name, int cd, int spellPower)
        {
            Name = name;
            Cd = cd;
            SpellPower = spellPower;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public int Cd
        {
            get { return this.cd; }
            set { this.cd = value; }
        }
        public IHero Caster
        {
            get { return this.caster; }
            set
            {
                this.caster = value;
            }
        }
        public int SpellPower
        {
            get { return this.spellPower; }
            set { this.spellPower = value; }
        }

        //FIX : printing logic
        public void Incantation()
        {
            result = RandomProvider.Generate(this.Caster.DmgStartOfRange, this.caster.DmgEndOfRange).ToString();

            caster.Opponent.HealthPoints -= int.Parse(result) + this.spellPower;
        }

        public override string ToString()
        {
            var total = int.Parse(result) + spellPower;
            return $"deals {total} damage";
        }
    }
}
