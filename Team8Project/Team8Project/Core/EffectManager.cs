using System;
using System.Linq;
using Team8Project.Common;
using Team8Project.Contracts;

namespace Team8Project.Core
{
    public class EffectManager
    {
        private static EffectManager instance;
        private EffectManager()
        {
        }

        //TODO : MESSAGES
        public int TransformDamage(int damage, IHero activeHero)
        {
            foreach (var effect in activeHero.AppliedEffects)
            {
                switch (effect.Type)
                {
                    case EffectType.Buff:
                        damage += effect.AbilityPower;
                    //    effect.Duration = 0;
                        break;
                    case EffectType.Debuff:
                        damage -= effect.AbilityPower;
                    //    effect.Duration = 0;
                        break;
                }
            }
            foreach (var effect in activeHero.Opponent.AppliedEffects)
            {
                switch (effect.Type)
                {
                    case EffectType.Resistance:
                        damage = 0;
                        activeHero.AppliedEffects.Remove(activeHero.Opponent.AppliedEffects.First(x => x.Type == EffectType.Resistance));
                        //check if linq is correct/ TODO : MESSAGES 
                        break;
                }
            }
            return damage;
        }
        public void AtTurnStart(IHero activeHero)
        {
            foreach (var effect in activeHero.AppliedEffects)
            {
                switch (effect.Type)
                {
                    case EffectType.DOT:
                        activeHero.HealthPoints -= effect.AbilityPower;
                        break;
                    case EffectType.HOT:
                        activeHero.HealthPoints += effect.AbilityPower;
                        break;

                    case EffectType.Incapacitated:
                        Console.WriteLine($"You are {effect.Type.ToString()}, you cannot do anything!"); //TODO : FIX MESSAGE
                        TurnProcessor.Instance.EndTurn();
                        break;
                }
            }
        }


        public static EffectManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EffectManager();
                }
                return instance;
            }
        }
    }
}
