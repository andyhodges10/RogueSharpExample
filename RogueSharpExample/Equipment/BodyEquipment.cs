namespace RogueSharpExample.Equipment
{
    public class BodyEquipment : Core.Equipment
    {
        public static BodyEquipment None()
        {
            return new BodyEquipment { Name = "None" };
        }

        public static BodyEquipment Leather()
        {
            return new BodyEquipment()
            {
                Defense = 1,
                Name = "Leather",
                Description = "Tunic",
                Value = 3
            };
        }

        public static BodyEquipment Chain()
        {
            return new BodyEquipment()
            {
                Defense = 1,
                MaxHealth = 2,
                Name = "Chain",
                Description = "Shirt",
                Value = 6
            };
        }

        public static BodyEquipment Chintin()
        {
            return new BodyEquipment()
            {
                Defense = 2,
                MaxHealth = 2,
                Name = "Chintin",
                Description = "Mail",
                Value = 9
            };
        }

        public static BodyEquipment Scaled()
        {
            return new BodyEquipment()
            {
                Defense = 2,
                MaxHealth = 3,
                MaxMana = 2,
                Name = "Scaled",
                Description = "Mail",
                Value = 12
            };
        }

        public static BodyEquipment Plate()
        {
            return new BodyEquipment()
            {
                Defense = 2,
                MaxHealth = 4,
                MaxMana = 3,
                Name = "Plate",
                Description = "Mail",
                Value = 15
            };
        }

        public static BodyEquipment Mithril()
        {
            return new BodyEquipment()
            {
                Defense = 3,
                MaxHealth = 5,
                MaxMana = 5,
                Name = "Mithril",
                Description = "Mail",
                Value = 25
            };
        }

        public static BodyEquipment DragonLord()
        {
            return new BodyEquipment()
            {
                Defense = 3,
                MaxHealth = 10,
                MaxMana = 10,
                Name = "DragonLord",
                Description = "Armor",
                GrantsPoisonImmunity = true,
                Value = 40
            };
        }
    }
}
