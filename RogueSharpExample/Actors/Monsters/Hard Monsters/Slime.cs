using RogueSharp.DiceNotation;
using RogueSharpExample.Behaviors;
using RogueSharpExample.Core;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Monsters
{
    public class Slime : Monster
    {
        public static Slime Create(int level)
        {
            int health = Dice.Roll("6D4");
            return new Slime {
                AttackMessages = new string[] { "The Slime tries to slime you", "The Slime tries to engulf you" },
                GreetMessages = new string[] { "The Slime starts rolling towards you", "The slime produces a weird gurgling noise" },
                DeathMessages = new string[] { "The Slime dissolves", "The Slime explodes" },
                Attack = Dice.Roll("4D3") + level / 2,
                AttackChance = Dice.Roll("10D6"),
                Awareness = 10,
                Color = Colors.SlimeColor,
                Defense = Dice.Roll("1D3") + level / 3,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("3D4"),
                Health = health,
                MaxHealth = health,
                Name = "Slime",
                Speed = 16,
                Experience = Dice.Roll("2D2"),
                IsPoisonedImmune = true,
                Symbol = 'S'
            };
        }

        public override void PerformAction(CommandSystem commandSystem)
        {
            var splitOozeBehavior = new SplitSludge();
            if (!splitOozeBehavior.Act(this, commandSystem))
            {
                base.PerformAction(commandSystem);
            }
        }
    }
}
