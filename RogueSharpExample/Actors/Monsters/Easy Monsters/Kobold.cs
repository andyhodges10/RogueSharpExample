using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Monsters
{
    public class Kobold : Monster
    {
        public static Kobold Create(int level)
        {
            int health = Dice.Roll("2D3");
            return new Kobold {
                AttackMessages = new string[] { "The Kobold swings its club at you" },
                GreetMessages = new string[] { "The Kobold looks in your direction and sizes you up", "The Kobold grins at you" },
                DeathMessages = new string[] { "The Kobold screams and falls down to the floor" },
                Attack = Dice.Roll("1D3") + level / 3,
                AttackChance = Dice.Roll("25D3"),
                Awareness = 10,
                Color = Colors.KoboldColor,
                Defense = Dice.Roll("1D2") + level / 3,
                DefenseChance = Dice.Roll("10D3"),
                Gold = Dice.Roll("5D4"),
                Health = health,
                MaxHealth = health,
                Name = "Kobold",
                Speed = 14,
                Experience = Dice.Roll("1D3") + level / 2,
                Symbol = 'k'
            };
        }
    }
}
