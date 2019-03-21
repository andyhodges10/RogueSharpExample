using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Monsters
{
    public class Wolf : Monster
    {
        public static Wolf Create(int level)
        {
            int health = Dice.Roll("4D3") + level / 2;
            return new Wolf {
                AttackMessages = new string[] { "The Wolf swings its paw at you", "The Wolf goes in for a bite" },
                GreetMessages = new string[] { "The Wolf goes into an alerted stance as it notices you" },
                DeathMessages = new string[] { "The Wolf yelps and crumbles to the ground" },
                Attack = Dice.Roll("2D2") + level / 3,
                AttackChance = Dice.Roll("25D4"),
                Awareness = 13,
                Color = Colors.WolfColor,
                Defense = Dice.Roll("1D2") + level / 3,
                DefenseChance = Dice.Roll("15D2"),
                Gold = Dice.Roll("5D5") + level / 2,
                Health = health,
                MaxHealth = health,
                Name = "Wolf",
                Speed = 8,
                Experience = Dice.Roll("2D3") + level / 2,
                Symbol = 'w'
            };
        }
    }
}
