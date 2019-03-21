using RogueSharp.DiceNotation;
using RogueSharpExample.Behaviors;
using RogueSharpExample.Core;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Monsters
{
    public class Ogre : Monster
    {
        private int? _turnsSpentRunning = null;
        private bool _shoutedForHelp = false;

        public static Ogre Create(int level)
        {
            int health = Dice.Roll("5D5") + level / 2;
            return new Ogre {
                AttackMessages = new string[] { "The Ogre swings its club at you" },
                GreetMessages = new string[] { "The Orc points its club at you" },
                DeathMessages = new string[] { "The Orc cries in agony as you land your killing blow" },
                Attack = Dice.Roll("5D4") + level / 2,
                AttackChance = Dice.Roll("15D4"),
                Awareness = 13,
                Color = Colors.OgreColor,
                Defense = Dice.Roll("2D2") + level / 3,
                DefenseChance = Dice.Roll("10D5"),
                Gold = Dice.Roll("7D4") + level / 2,
                Health = health,
                MaxHealth = health,
                Name = "Ogre",
                Speed = 17,
                Experience = Dice.Roll("4D3") + level / 2,
                Symbol = 'O'
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
