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
                Description = "Hat",
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
                Description = "Helmet",
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
                Description = "Helmet",
                Value = 9
            };
        }

        public static HeadEquipment Scaled()
        {
            return new HeadEquipment()
            {
                DefenseChance = 4,
                MaxHealth = 3,
                MaxMana = 2,
                Name = "Scaled",
                Description = "Helmet",
                Value = 12
            };
        }

        public static HeadEquipment Plate()
        {
            return new HeadEquipment()
            {
                DefenseChance = 5,
                MaxHealth = 3,
                MaxMana = 4,
                Name = "Plate",
                Description = "Helmet",
                Value = 15
            };
        }

        public static HeadEquipment Mithril()
        {
            return new HeadEquipment()
            {
                DefenseChance = 5,
                MaxHealth = 5,
                MaxMana = 10,
                Name = "Mithril",
                Description = "Helmet",
                Value = 25
            };
        }
        public static HeadEquipment DragonLord()
        {
            return new HeadEquipment()
            {
                Defense = 1,
                DefenseChance = 5,
                MaxHealth = 5,
                MaxMana = 15,
                Name = "DragonLord",
                Description = "Helmet",
                Value = 40
            };
        }
    }
}
