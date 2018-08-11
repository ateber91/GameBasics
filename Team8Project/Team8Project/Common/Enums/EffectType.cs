namespace Team8Project.Common.Enums
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
    }
}
