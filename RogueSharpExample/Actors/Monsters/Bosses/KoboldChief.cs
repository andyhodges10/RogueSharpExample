using RogueSharpExample.Core;

namespace RogueSharpExample.Monsters
{
    public class KoboldChief : Monster
    {
        public static KoboldChief Create(int level)
        {
            int health = 40;
            return new KoboldChief {
                AttackMessages = new string[] { "The Kobold swings its staff at you" },
                GreetMessages = new string[] { "The Kobold Chief curses at you in a foreign tongue" },
                DeathMessages = new string[] { "The Kobold Chief screams as you land your finishing blow" },
                Attack = 6,
                AttackChance = 55,
                Awareness = 4,
                Color = Colors.KoboldColor,
                Defense = 3,
                DefenseChance = 35,
                Gold = 55,
                Health = health,
                MaxHealth = health,
                Name = "Kobold Chief",
                Speed = 9,
                Experience = 20,
                IsABoss = true,
                Symbol = 'K'
            };
        }
    }
}
