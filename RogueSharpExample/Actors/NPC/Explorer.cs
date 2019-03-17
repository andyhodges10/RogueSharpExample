namespace RogueSharpExample.Core
{
    public class Explorer : Actor
    {
        public static Explorer Create(int level, string[] greetMessages)
        {
            return new Explorer {
                Attack = 20,
                AttackChance = 90,
                Awareness = 16,
                Defense = 16,
                DefenseChance = 90,
                Gold = 0,
                Health = 300,
                MaxHealth = 300,
                Speed = 0,
                Name = "Explorer",
                Color = Colors.NPC,
                GreetMessages = greetMessages,
                Symbol = (char)1 // ☺
            };
        }
    }
}
