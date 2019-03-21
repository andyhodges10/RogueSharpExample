using RogueSharp.DiceNotation;
using RogueSharpExample.Behaviors;
using RogueSharpExample.Core;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Monsters
{
    class Dragon : Monster
    {
        private bool _didPoison = false;

        public static Dragon Create(int level)
        {
            int health = Dice.Roll("5D6") + level / 2;
            return new Dragon {
                AttackMessages = new string[] { "The Dragon thrusts its maw at you" },
                GreetMessages = new string[] { "The Dragon locks it's eyes with yours" },
                DeathMessages = new string[] { "The Dragon collapses never to rise again", "The Dragon lets out a roar and falls down dead" },
                Attack = Dice.Roll("5D4") + level / 2,
                AttackChance = Dice.Roll("15D5"),
                Awareness = 14,
                Color = Colors.DragonColor,
                Defense = Dice.Roll("2D2") + level / 3,
                DefenseChance = Dice.Roll("10D5"),
                Gold = Dice.Roll("8D4") + level / 2,
                Health = health,
                MaxHealth = health,
                Name = "Dragon",
                Speed = 10,
                Experience = Dice.Roll("5D3") + level / 2,
                PoisonDamage = 4,
                PoisonChance = 75,
                PoisonLength = 6,
                IsPoisonedImmune = true,
                Symbol = 'd'
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
