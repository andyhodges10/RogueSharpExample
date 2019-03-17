using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Monsters
{
    public class KoboldChief : Monster
    {
        public static KoboldChief Create(int level)
        {
            int health = Dice.Roll("5D5");
            return new KoboldChief {
                AttackMessages = new string[] { "The Kobold swings its staff at you" },
                GreetMessages = new string[] { "The Kobold Chief curses at you in a foreign tongue" },
                DeathMessages = new string[] { "The Kobold Chief screams as you land your finishing blow" },
                Attack = Dice.Roll("1D4") + level / 3,
                AttackChance = Dice.Roll("30D3"),
                Awareness = 4,
                Color = Colors.KoboldColor,
                Defense = Dice.Roll("1D2") + level / 3,
                DefenseChance = Dice.Roll("10D3"),
                Gold = Dice.Roll("5D5"),
                Health = health,
                MaxHealth = health,
                Name = "Kobold Chief",
                Speed = 14,
                Experience = Dice.Roll("4D3") + level / 2,
                IsABoss = true,
                Symbol = 'K'
            };
        }
    }
}
