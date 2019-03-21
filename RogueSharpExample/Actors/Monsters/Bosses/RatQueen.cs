using RogueSharpExample.Behaviors;
using RogueSharpExample.Core;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Monsters
{
    public class RatQueen : Monster
    {
        private bool _didPoison = false;

        public static RatQueen Create(int level)
        {
            int health = 26;
            return new RatQueen {
                AttackMessages = new string[] { "The Rat-ant Queen opens its mouth and charges you", "The Rat-ant Queen pounces you" },
                GreetMessages = new string[] { "The Rat-ant Queen makes a few loud and rapid squaks at you before it locks all of its focus on you" },
                DeathMessages = new string[] { "The Rat-ant Queen lets out a shrill cry and scatters away only to die shortly after" },
                Attack = 4,
                AttackChance = 50,
                Awareness = 3,
                Color = Colors.RatColor,
                Defense = 2,
                DefenseChance = 30,
                Gold = 35,
                Health = health,
                MaxHealth = health,
                Name = "Rat-ant Queen",
                Speed = 9,
                Experience = 15,
                PoisonDamage = 3,
                PoisonChance = 65,
                PoisonLength = 6,
                IsPoisonedImmune = true,
                IsABoss = true,
                Symbol = 'R'
            };
        }

        public override void PerformAction(CommandSystem commandSystem)
        {
            var monsterPoisonBehavior = new MonsterPoison();

            if (Health < MaxHealth / 2 && _didPoison == false)
            {
                _didPoison = monsterPoisonBehavior.Act(this, commandSystem);
            }
            else
            {
                base.PerformAction(commandSystem);
            }
        }
    }
}
