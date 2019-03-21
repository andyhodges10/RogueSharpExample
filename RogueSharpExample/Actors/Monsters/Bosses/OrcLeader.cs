using RogueSharpExample.Core;

namespace RogueSharpExample.Monsters
{
    public class OrcLeader : Monster
    {
        public static OrcLeader Create(int level)
        {
            int health = 80;
            return new OrcLeader  { 
                AttackMessages = new string[] { "The Orc Leader swings its waraxe at you" },
                GreetMessages = new string[] { "The Orc Leader sharpens its waraxe out its sharp blade" },
                DeathMessages = new string[] { "The Orc leader cries in agony as you land your killing blow" },
                Attack = 12,
                AttackChance = 70,
                Awareness = 6,
                Color = Colors.OrcColor,
                Defense = 5,
                DefenseChance = 50,
                Gold = 90,
                Health = health,
                MaxHealth = health,
                Name = "Orc Leader",
                Speed = 9,
                Experience = 30,
                IsABoss = true,
                Symbol = 'O'
            };
        }
    }
}
