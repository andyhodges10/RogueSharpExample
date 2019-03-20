namespace RogueSharpExample.Interfaces
{
    public interface IEquipment
    {
        int Attack { get; set; }
        int AttackChance { get; set; }
        int Awareness { get; set; }
        int Defense { get; set; }
        int DefenseChance { get; set; }
        int MaxHealth { get; set; }
        int MaxMana { get; set; }
        string Name { get; set; }
        int Speed { get; set; }
        bool GrantsPoisonImmunity { get; set; }
        bool IsEnhanced { get; set; } // Currently unused
        int Value { get; set; }
    }
}
