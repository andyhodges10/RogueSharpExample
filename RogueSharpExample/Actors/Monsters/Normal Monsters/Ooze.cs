using RogueSharp.DiceNotation;
using RogueSharpExample.Behaviors;
using RogueSharpExample.Core;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Monsters
{
    public class Ooze : Monster
    {
        public static Ooze Create(int level)
        {
            int health = Dice.Roll("4D5");
            return new Ooze {
                AttackMessages = new string[] { "The Ooze tries to slime you", "The Ooze tries to engulf you" },
                GreetMessages = new string[] { "The Ooze bubbles evilly", "The Ooze produces a weird gurgling noise" },
                DeathMessages = new string[] { "The Ooze dissolves", "The Slime explodes" },
                Attack = Dice.Roll("1D4") + level / 3,
                AttackChance = Dice.Roll("10D5"),
                Awareness = 10,
                Color = Colors.OozeColor,
                Defense = Dice.Roll("1D2") + level / 3,
                DefenseChance = Dice.Roll("10D3"),
                Gold = Dice.Roll("1D20"),
                Health = health,
                MaxHealth = health,
                Name = "Ooze",
                Speed = 14,
                Experience = Dice.Roll("1D2") + level / 3,
                IsPoisonedImmune = true,
                Symbol = 'o'
            };
        }

        public override void PerformAction(CommandSystem commandSystem)
        {
            var splitOozeBehavior = new SplitOoze();
            if (!splitOozeBehavior.Act(this, commandSystem))
            {
                base.PerformAction(commandSystem);
            }
        }
    }
}
