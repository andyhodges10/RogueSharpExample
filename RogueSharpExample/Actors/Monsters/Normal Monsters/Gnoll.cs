using RogueSharp.DiceNotation;
using RogueSharpExample.Behaviors;
using RogueSharpExample.Core;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Monsters
{
    public class Gnoll : Monster
    {
        private int? _turnsSpentRunning = null;
        private bool _shoutedForHelp = false;

        public static Gnoll Create(int level)
        {
            int health = Dice.Roll("4D4") + level / 2;
            return new Gnoll {
                AttackMessages = new string[] { "The Gnoll swings its sword at you" },
                GreetMessages = new string[] { "The Gnoll sharpens it's blade" },
                DeathMessages = new string[] { "The Gnoll falls down dead" },
                Attack = Dice.Roll("3D3") + level / 3,
                AttackChance = Dice.Roll("13D4"),
                Awareness = 10,
                Color = Colors.GnollColor,
                Defense = Dice.Roll("1D2") + level / 3,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("5D7") + level / 2,
                Health = health,
                MaxHealth = health,
                Name = "Gnoll",
                Speed = 14 - level / 3,
                Experience = Dice.Roll("2D4") + level / 2,
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
            else if (Health < MaxHealth / 3)
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
