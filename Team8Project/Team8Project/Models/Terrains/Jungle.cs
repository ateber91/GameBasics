using System.Linq;
using System.Text;
using Team8Project.Common.Enums;
using Team8Project.Contracts;
using Team8Project.Core;

namespace Team8Project.Models.Terrains
{
    public class Jungle : Terrain
    {
        public Jungle() { }

        public override void ApplyInitialEffect(IHero hero)
        {
            hero.InitializeJungle();
        }

        public override string ContinuousEffect(IHero hero)
        {
            if (this.IsDay == true)
            {
                foreach (var ability in hero.Abilities.Skip(3))
                {
                    ability.AbilityPower += 5;
                }
                return $"{hero.Name}'s damaging abilities power increased by 5";
            }
            else
            {
                foreach (var ability in hero.Abilities.Skip(3))
                {
                    ability.AbilityPower -= 2;
                }
                return $"{hero.Name}'s damaging abilities power decreased by 2";
            }
        }
    }
}
