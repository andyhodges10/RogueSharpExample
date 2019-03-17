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
                Name2 = "Tunic",
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
                Name2 = "Shirt",
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
                Name2 = "mail",
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
                Name2 = "mail",
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
                Name2 = "mail",
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
                Name2 = "mail",
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
                Name2 = "Armor",
                GrantsPoisonImmunity = true,
                Value = 35
            };
        }
    }
}
