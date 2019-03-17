using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Monsters
{
    public class LizardMan : Monster
    {
        public static LizardMan Create(int level)
        {
            int health = Dice.Roll("4D5");
            return new LizardMan {
                AttackMessages = new string[] { "The Lizardman thrusts its pike at you" },
                GreetMessages = new string[] { "The Lizardman takes out its sharp blade" },
                DeathMessages = new string[] { "The Lizardman cries in pain as you land your killing blow" },
                Attack = Dice.Roll("3D4") + level / 3,
                AttackChance = Dice.Roll("15D4"),
                Awareness = 12,
                Color = Colors.LizardmanColor,
                Defense = Dice.Roll("1D3") + level / 3,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("5D4"),
                Health = health,
                MaxHealth = health,
                Name = "LizardMan",
                Speed = 11,
                Experience = Dice.Roll("2D5") + level / 2,
                Symbol = 'L'
            };
        }
    }
}
