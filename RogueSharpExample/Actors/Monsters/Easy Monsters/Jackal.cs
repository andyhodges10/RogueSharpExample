using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Monsters
{
    public class Jackal : Monster
    {
        public static Jackal Create(int level)
        {
            int health = Dice.Roll("2D4");
            return new Jackal {
                AttackMessages = new string[] { "The Jackal swings its paw at you", "The Jackal goes in for a bite" },
                GreetMessages = new string[] { "The Jackal assumes it's hunting stance as it notices you", "The Jackal assumes it's combat stance" },
                DeathMessages = new string[] { "The Jackal yelps and crumbles to the ground" },
                Attack = Dice.Roll("1D2") + level / 3,
                AttackChance = Dice.Roll("20D3"),
                Awareness = 12,
                Color = Colors.JackalColor,
                Defense = Dice.Roll("1D1") + level / 3,
                DefenseChance = Dice.Roll("10D2"),
                Gold = Dice.Roll("5D3"),
                Health = health,
                MaxHealth = health,
                Name = "Jackal",
                Speed = 9,
                Experience = Dice.Roll("1D2") + level / 2,
                Symbol = 'j'
            };
        }
    }
}
