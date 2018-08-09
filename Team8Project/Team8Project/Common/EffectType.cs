using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team8Project.Contracts;

namespace Team8Project.Common
{
    public enum EffectType
    {
        Damage = 0,
        DOT = 1, //Damage over time
        HOT = 2, //Healing over time
        Incapacitated = 3, //Cannot act this turn
        Resistance = 4, // Takes 0 damage
        Buff = 5,// Possitive application
        Debuff = 6 //Negative application
        //Critical = 1,
        //Poison = 2,
        //Evasion = 3,
        //Bleed = 4,
        //Block = 5,
        //Stunned = 6,
        //IceBarrier = 7,
        //Burn = 8,
        //Frozen = 9,
        //Regeneration = 10,
        //Cursed = 11,
        //Blessed = 12

    }
}
