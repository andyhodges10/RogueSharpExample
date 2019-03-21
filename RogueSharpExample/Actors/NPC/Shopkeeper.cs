namespace RogueSharpExample.Core
{
    public class Shopkeeper : Actor
    {
        public static Shopkeeper Create(int level)
        {
            return new Shopkeeper {
                Attack = 20,
                AttackChance = 90,
                Awareness = 16,
                Defense = 16,
                DefenseChance = 90,
                Gold = 0,
                Health = 300,
                MaxHealth = 300,
                Speed = 0,
                Name = "Shopkeeper",
                Color = Colors.NPC,
                Symbol = (char)1 // ☺
            };
        }
    }
}
