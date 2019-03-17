using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Monsters
{
    public class GnollGiant : Monster
    {
        public static GnollGiant Create(int level)
        {
            int health = Dice.Roll("6D5");
            return new GnollGiant {
                AttackMessages = new string[] { "The Gnoll Giant swings its spike club at you" },
                GreetMessages = new string[] { "The Gnoll Giant grins at you" },
                DeathMessages = new string[] { "The Gnoll Giant screams and falls down dead" },
                Attack = Dice.Roll("2D3") + level / 3,
                AttackChance = Dice.Roll("30D3"),
                Awareness = 6,
                Color = Colors.KoboldColor,
                Defense = Dice.Roll("1D2") + level / 3,
                DefenseChance = Dice.Roll("10D3"),
                Gold = Dice.Roll("6D5"),
                Health = health,
                MaxHealth = health,
                Name = "Giant Gnoll",
                Speed = 11,
                Experience = Dice.Roll("4D4") + level / 2,
                IsABoss = true,
                Symbol = 'G'
            };
        }
    }
}
