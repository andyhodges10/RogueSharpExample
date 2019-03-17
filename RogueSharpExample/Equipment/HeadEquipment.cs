namespace RogueSharpExample.Equipment
{
    public class HeadEquipment : Core.Equipment
    {
        public static HeadEquipment None()
        {
            return new HeadEquipment { Name = "None" };
        }

        public static HeadEquipment Leather()
        {
            return new HeadEquipment()
            {
                DefenseChance = 3,
                Name = "Leather",
                Name2 = "Hat",
                Value = 3
            };
        }

        public static HeadEquipment Chain()
        {
            return new HeadEquipment()
            {
                DefenseChance = 3,
                MaxHealth = 2,
                Name = "Chain",
                Name2 = "Helmet",
                Value = 6
            };
        }

        public static HeadEquipment Chintin()
        {
            return new HeadEquipment()
            {
                DefenseChance = 4,
                MaxHealth = 2,
                Name = "Chintin",
                Name2 = "Helmet",
                Value = 9
            };
        }

        public static HeadEquipment Scaled()
        {
            return new HeadEquipment()
            {
                DefenseChance = 5,
                MaxHealth = 3,
                MaxMana = 2,
                Name = "Scaled",
                Name2 = "Helmet",
                Value = 12
            };
        }

        public static HeadEquipment Plate()
        {
            return new HeadEquipment()
            {
                DefenseChance = 5,
                MaxHealth = 5,
                MaxMana = 2,
                Name = "Plate",
                Name2 = "Helmet",
                Value = 15
            };
        }

        public static HeadEquipment Mithril()
        {
            return new HeadEquipment()
            {
                Defense = 1,
                DefenseChance = 5,
                MaxHealth = 5,
                MaxMana = 5,
                Name = "Mithril",
                Name2 = "Helmet",
                Value = 25
            };
        }
    }
}
