using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Monsters
{
    class Dragon : Monster
    {
        public static Dragon Create(int level)
        {
            int health = Dice.Roll("5D6");
            return new Dragon {
                AttackMessages = new string[] { "The Dragon thrusts its maw at you" },
                GreetMessages = new string[] { "The Dragon locks it's eyes with yours" },
                DeathMessages = new string[] { "The Dragon collapses never to rise again" },
                Attack = Dice.Roll("4D4") + level / 3,
                AttackChance = Dice.Roll("15D5"),
                Awareness = 14,
                Color = Colors.DragonColor,
                Defense = Dice.Roll("1D5") + level / 3,
                DefenseChance = Dice.Roll("10D5"),
                Gold = Dice.Roll("5D4"),
                Health = health,
                MaxHealth = health,
                Name = "Dragon",
                Speed = 10,
                Experience = Dice.Roll("2D6") + level / 2,
                Symbol = 'd'
            };
        }
    }
}
