namespace RogueSharpExample.Equipment
{
    public class FeetEquipment : Core.Equipment
    {
        public static FeetEquipment None()
        {
            return new FeetEquipment { Name = "None" };
        }

        public static FeetEquipment Leather()
        {
            return new FeetEquipment()
            {
                DefenseChance = 3,
                Name = "Leather",
                Description = "Boots",
                Value = 3
            };
        }

        public static FeetEquipment Chain()
        {
            return new FeetEquipment()
            {
                DefenseChance = 6,
                Name = "Chain",
                Description = "Boots",
                Value = 6
            };
        }

        public static FeetEquipment Chintin()
        {
            return new FeetEquipment()
            {
                DefenseChance = 8,
                Name = "Chintin",
                Description = "Boots",
                Value = 9
            };
        }

        public static FeetEquipment Scaled()
        {
            return new FeetEquipment()
            {
                DefenseChance = 10,
                Name = "Scaled",
                Description = "Boots",
                Value = 12
            };
        }

        public static FeetEquipment Plate()
        {
            return new FeetEquipment()
            {
                DefenseChance = 12,
                Name = "Plate",
                Description = "Boots",
                Value = 15
            };
        }

        public static FeetEquipment Mithril()
        {
            return new FeetEquipment()
            {
                DefenseChance = 12,
                MaxMana = 5,
                Name = "Mithril",
                Description = "Boots",
                Value = 25
            };
        }

        public static FeetEquipment DragonLord()
        {
            return new FeetEquipment()
            {
                Speed = -2,
                DefenseChance = 15,
                MaxMana = 5,
                Name = "DragonLord",
                Description = "Boots",
                Value = 40
            };
        }
    }
}
