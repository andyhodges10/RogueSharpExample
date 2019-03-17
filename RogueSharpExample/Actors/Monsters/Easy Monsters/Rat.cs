using RogueSharp.DiceNotation;
using RogueSharpExample.Behaviors;
using RogueSharpExample.Core;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Monsters
{
    public class Rat : Monster
    {
        private bool _didPoison = false;

        public static Rat Create(int level)
        {
            int health = Dice.Roll("2D4");
            return new Rat {
                AttackMessages = new string[] { "The Rat-ant opens its mouth and charges you" },
                GreetMessages = new string[] { "The giant Rat-ant looks menacingly at you" },
                DeathMessages = new string[] { "The Rat-ant squaks loudly and falls over dead" },
                Attack = Dice.Roll("1D2") + level / 3,
                AttackChance = Dice.Roll("20D3"),
                Awareness = 6,
                Color = Colors.RatColor,
                Defense = Dice.Roll("1D1") + level / 3,
                DefenseChance = Dice.Roll("10D2"),
                Gold = Dice.Roll("5D3"),
                Health = health,
                MaxHealth = health,
                Name = "Rat-ant",
                Speed = 11,
                Experience = Dice.Roll("1D2") + level / 2,
                PoisonDamage = 2,
                IsPoisonedImmune = true,
                Symbol = 'r'
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
