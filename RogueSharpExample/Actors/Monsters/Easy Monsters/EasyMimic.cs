using RogueSharp.DiceNotation;
using RogueSharpExample.Behaviors;
using RogueSharpExample.Core;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Monsters
{
    public class EasyMimic : Monster
    {
        private bool _didReveal = false;

        public static Mimic Create(int level)
        {
            int health = Dice.Roll("4D3") + level / 2;
            return new Mimic
            {
                GreetMessages = new string[] { "The Mimic rapidly shifts it's shape" },
                DeathMessages = new string[] { "The Mimic produces a booming cry and reveals its true form as it dies" },
                Attack = Dice.Roll("2D2") + level / 2,
                AttackChance = Dice.Roll("12D6"),
                Awareness = 10,
                Color = Colors.MimicColor,
                Defense = Dice.Roll("1D2") + level / 3,
                DefenseChance = Dice.Roll("10D3"),
                Gold = Dice.Roll("5D3") + level / 2,
                Health = health,
                MaxHealth = health,
                Name = "Plate Body-Armor",
                Speed = 11,
                Experience = Dice.Roll("3D2") + level / 2,
                PoisonDamage = 2,
                IsPoisonedImmune = true,
                IsMimicInHiding = true,
                Symbol = ']'
            };
        }

        public override void PerformAction(CommandSystem commandSystem)
        {
            var mimicRevealedBehavior = new StandardMoveAndAttack();
            var mimicDisguisedBehavior = new MimicDisguised();

            if (_didReveal == false)
            {
                _didReveal = mimicDisguisedBehavior.Act(this, commandSystem);
            }
            else
            {
                mimicRevealedBehavior.Act(this, commandSystem);
            }
        }
    }
}
