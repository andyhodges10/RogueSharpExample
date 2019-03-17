using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Monsters
{
    public class Lichen : Monster
    {
        public static Lichen Create(int level)
        {
            int health = Dice.Roll("3D3");
            return new Lichen {
                AttackMessages = new string[] { "The litchen shoots its spores at you" },
                GreetMessages = new string[] { "The Litchen slowly inches towards your direction " },
                DeathMessages = new string[] { "The Litchen dissolves " },
                Attack = Dice.Roll("2D2") + level / 3,
                AttackChance = Dice.Roll("33D3"),
                Awareness = 9,
                Color = Colors.LichenColor,
                Defense = Dice.Roll("1D2") + level / 3,
                DefenseChance = Dice.Roll("10D3"),
                Gold = Dice.Roll("4D4"),
                Health = health,
                MaxHealth = health,
                Name = "Lichen",
                Speed = 50,
                Experience = Dice.Roll("2D2") + level / 2,
                IsPoisonedImmune = true,
                Symbol = 'l'
            };
        }
    }
}
