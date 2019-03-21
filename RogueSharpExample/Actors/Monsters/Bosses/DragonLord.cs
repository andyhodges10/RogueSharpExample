using RogueSharpExample.Behaviors;
using RogueSharpExample.Core;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Monsters
{
    class DragonLord : Monster
    {
        private bool _didPoison = false;

        public static DragonLord Create(int level)
        {
            int health = 100;
            return new DragonLord {
                AttackMessages = new string[] { "The DragonLord thrusts its tail at you", "The DragonLord swings its sword at you", "The DragonLord goes in for a bite" },
                GreetMessages = new string[] { "The DragonLord stands talls and assumes its battle stance", "The DragonLord points at you and draws its Magical Sword" },
                DeathMessages = new string[] { "The DragonLord falls over dead. You win", "You finally have defated the DragonLord, your quest is complete" },
                Attack = 16,
                AttackChance = 75,
                Awareness = 7,
                Color = Colors.DragonColor,
                Defense = 7,
                DefenseChance = 60,
                Gold = 300,
                Health = health,
                MaxHealth = health,
                Name = "DragonLord",
                Speed = 10,
                Experience = 40,
                PoisonDamage = 6,
                PoisonChance = 98,
                PoisonLength = 7,
                IsPoisonedImmune = true,
                IsABoss = true,
                IsEndBoss = true,
                Symbol = 'D'
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
