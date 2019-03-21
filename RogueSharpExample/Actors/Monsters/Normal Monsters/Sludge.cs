using RogueSharp.DiceNotation;
using RogueSharpExample.Behaviors;
using RogueSharpExample.Core;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Monsters
{
    public class Sludge : Monster
    {
        public static Sludge Create(int level)
        {
            int health = Dice.Roll("4D3");
            return new Sludge {
                AttackMessages = new string[] { "The Sludge tries to slime you", "The Sludge tries to engulf you" },
                GreetMessages = new string[] { "The Sludge bubbles evilly", "The Sludge produces a weird gurgling noise" },
                DeathMessages = new string[] { "The Sludge dissolves", "The Slime explodes" },
                Attack = Dice.Roll("3D2") + level / 3,
                AttackChance = Dice.Roll("10D5"),
                Awareness = 10,
                Color = Colors.SludgeColor,
                Defense = Dice.Roll("1D2") + level / 3,
                DefenseChance = Dice.Roll("10D3"),
                Gold = Dice.Roll("2D4"),
                Health = health,
                MaxHealth = health,
                Name = "Sludge",
                Speed = 20,
                Experience = Dice.Roll("1D2"),
                IsPoisonedImmune = true,
                Symbol = 's'
            };
        }

        public override void PerformAction(CommandSystem commandSystem)
        {
            var splitSludgeBehavior = new SplitSludge();
            if (!splitSludgeBehavior.Act(this, commandSystem))
            {
                base.PerformAction(commandSystem);
            }
        }
    }
}
