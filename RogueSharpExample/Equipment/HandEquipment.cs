using RogueSharpExample.Core;
using RogueSharpExample.Abilities;

namespace RogueSharpExample.Equipment
{
    public class HandEquipment : Core.Equipment
    {
        public Ability AtkAbility { get; set; }

        public static HandEquipment None()
        {
            return new HandEquipment { Name = "None" };
        }

        public static HandEquipment Dagger()
        {
            return new HandEquipment
            {
                Attack = 1,
                AttackChance = 5,
                Name = "Dagger",
                Speed = -1,
                Value = 4
            };
        }

        public static HandEquipment Sword()
        {
            return new HandEquipment
            {
                Attack = 1,
                AttackChance = 10,
                Name = "Sword",
                Speed = -1,
                Value = 8
            };
        }

        public static HandEquipment Axe()
        {
            return new HandEquipment
            {
                Attack = 2,
                AttackChance = 15,
                Name = "Axe",
                Value = 12
            };
        }

        public static HandEquipment TwoHanded()
        {
            return new HandEquipment
            {
                Attack = 3,
                AttackChance = 15,
                Name = "2H Sword",
                Speed = 1,
                Value = 15
            };
        }

        public static HandEquipment Dragonbane()
        {
            return new HandEquipment
            {
                Attack = 4,
                AttackChance = 15,
                MaxMana = 2,
                Name = "Dragonbane",
                Value = 30
            };
        }

        public static HandEquipment DragonLord()
        {
            return new HandEquipment
            {
                Attack = 4,
                AttackChance = 20,
                MaxMana = 5,
                AtkAbility = new InflictPoison(70, 6, 2, "Inflict Poison"),
                Name = "DragonLord",
                Speed = -1,
                Value = 70
            };
        }

        public static HandEquipment Vampiric()
        {
            return new HandEquipment
            {
                Attack = 4,
                AttackChance = 20,
                MaxMana = 5,
                AtkAbility = new Lifesteal(75, "Lifesteal"),
                Name = "Vampiric",
                Speed = -1,
                Value = 70
            };
        }

    }
}
