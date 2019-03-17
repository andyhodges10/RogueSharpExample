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
                Name2 = "Boots",
                Value = 3
            };
        }

        public static FeetEquipment Chain()
        {
            return new FeetEquipment()
            {
                DefenseChance = 6,
                Name = "Chain",
                Name2 = "Boots",
                Value = 6
            };
        }

        public static FeetEquipment Chintin()
        {
            return new FeetEquipment()
            {
                DefenseChance = 8,
                Name = "Chintin",
                Name2 = "Boots",
                Value = 9
            };
        }

        public static FeetEquipment Scaled()
        {
            return new FeetEquipment()
            {
                DefenseChance = 10,
                MaxMana = 3,
                Name = "Scaled",
                Name2 = "Boots",
                Value = 12
            };
        }

        public static FeetEquipment Plate()
        {
            return new FeetEquipment()
            {
                DefenseChance = 12,
                MaxMana = 5,
                Name = "Plate",
                Name2 = "Boots",
                Value = 15
            };
        }

        public static FeetEquipment Mithril()
        {
            return new FeetEquipment()
            {
                DefenseChance = 15,
                MaxMana = 5,
                Name = "Mithril",
                Name2 = "Boots",
                Value = 25
            };
        }
    }
}
