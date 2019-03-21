using RogueSharpExample.Core;

namespace RogueSharpExample.Monsters
{
    public class GnollGiant : Monster
    {
        public static GnollGiant Create(int level)
        {
            int health = 55;
            return new GnollGiant {
                AttackMessages = new string[] { "The Gnoll Giant swings its spike club at you" },
                GreetMessages = new string[] { "The Gnoll Giant grins at you" },
                DeathMessages = new string[] { "The Gnoll Giant screams and falls down dead" },
                Attack = 9,
                AttackChance = 65,
                Awareness = 5,
                Color = Colors.GnollColor,
                Defense = 4,
                DefenseChance = 40,
                Gold = 75,
                Health = health,
                MaxHealth = health,
                Name = "Giant Gnoll",
                Speed = 10,
                Experience = 25,
                IsABoss = true,
                Symbol = 'G'
            };
        }
    }
}
