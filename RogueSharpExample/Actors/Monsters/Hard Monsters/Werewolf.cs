using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Monsters
{
    public class Werewolf : Monster
    {
        public static Werewolf Create(int level)
        {
            int health = Dice.Roll("4D4") + level / 2;
            return new Werewolf
            {
                AttackMessages = new string[] { "The Werewolf swings its paw at you", "The Wolf goes in for a bite" },
                GreetMessages = new string[] { "The Werewolf goes into an alerted stance as it notices you" },
                DeathMessages = new string[] { "The Werewolf yelps and crumbles to the ground" },
                Attack = Dice.Roll("4D2") + level / 2,
                AttackChance = Dice.Roll("25D4"),
                Awareness = 14,
                Color = Colors.WolfColor,
                Defense = Dice.Roll("2D2") + level / 3,
                DefenseChance = Dice.Roll("17D2"),
                Gold = Dice.Roll("8D3") + level / 2,
                Health = health,
                MaxHealth = health,
                Name = "Werewolf",
                Speed = 8,
                Experience = Dice.Roll("3D3") + level / 2,
                Symbol = 'W'
            };
        }
    }
}
