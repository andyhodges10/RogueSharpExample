using RogueSharp.DiceNotation;
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
            int health = Dice.Roll("4D4");
            return new RatQueen {
                AttackMessages = new string[] { "The Rat-ant Queen opens its mouth and charges you" },
                GreetMessages = new string[] { "The Rat-ant Queen makes a few loud and rapid squaks at you before it locks all of its focus on you" },
                DeathMessages = new string[] { "The Rat-ant Queen lets out a shrill cry and scatters away only to die shortly after" },
                Attack = Dice.Roll("1D3") + level / 3,
                AttackChance = Dice.Roll("20D3"),
                Awareness = 3,
                Color = Colors.RatColor,
                Defense = Dice.Roll("1D1") + level / 3,
                DefenseChance = Dice.Roll("10D2"),
                Gold = Dice.Roll("5D3"),
                Health = health,
                MaxHealth = health,
                Name = "Queen Rat-ant",
                Speed = 10,
                Experience = Dice.Roll("1D2") + level / 2,
                PoisonDamage = 3,
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
