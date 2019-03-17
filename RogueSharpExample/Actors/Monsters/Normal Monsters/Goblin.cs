using RogueSharp.DiceNotation;
using RogueSharpExample.Behaviors;
using RogueSharpExample.Core;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Monsters
{
    public class Goblin : Monster
    {
        private int? _turnsSpentRunning = null;
        private bool _shoutedForHelp = false;

        public static Goblin Create(int level)
        {
            int health = Dice.Roll("2D6");
            return new Goblin {
                AttackMessages = new string[] { "The Goblin swings its axe at you" },
                GreetMessages = new string[] { "The Goblin shouts at you" },
                DeathMessages = new string[] { "The Goblin falls down dead" },
                Attack = Dice.Roll("2D5") + level / 3,
                AttackChance = Dice.Roll("12D5"),
                Awareness = 10,
                Color = Colors.GoblinColor,
                Defense = Dice.Roll("1D2") + level / 3,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("4D4"),
                Health = health,
                MaxHealth = health,
                Name = "Goblin",
                Speed = 16,
                Experience = Dice.Roll("2D3") + level / 2,
                Symbol = 'g'
            };
        }

        public override void PerformAction(CommandSystem commandSystem)
        {
            var monsterHealBehavior = new MonsterHeal();
            var runAwayBehavior = new RunAway();
            var shoutForHelpBehavior = new ShoutForHelp();

            if (_turnsSpentRunning.HasValue && _turnsSpentRunning.Value > 4)
            {
                monsterHealBehavior.Act(this, commandSystem);
                _turnsSpentRunning = null;
            }
            else if (Health < MaxHealth / 2)
            {
                runAwayBehavior.Act(this, commandSystem);
                if (_turnsSpentRunning.HasValue)
                {
                    _turnsSpentRunning += 1;
                }
                else
                {
                    _turnsSpentRunning = 1;
                }

                if (!_shoutedForHelp)
                {
                    _shoutedForHelp = shoutForHelpBehavior.Act(this, commandSystem);
                }
            }
            else
            {
                base.PerformAction(commandSystem);
            }
        }
    }
}
