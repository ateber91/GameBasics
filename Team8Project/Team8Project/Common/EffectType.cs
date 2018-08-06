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
        HpBoost, //add hp to active hero
        AttackBoost, //add attack to active hero
        SpBoost, // add spell power to active hero
        Weakness, // removes attack from active hero
        Frozen, // active hero skips turn
        Block, // active hero does 0 direct damage to opponent
        Evasion, // active hero has 50% chance to miss targeting opponent
        Fatigue // active hero's opponent has reduced healthpoints

    }
}
