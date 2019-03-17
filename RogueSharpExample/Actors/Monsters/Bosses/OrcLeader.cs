using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Monsters
{
    public class OrcLeader : Monster
    {
        public static OrcLeader Create(int level)
        {
            int health = Dice.Roll("6D6");
            return new OrcLeader  {
                AttackMessages = new string[] { "The Orc Leader swings its waraxe at you" },
                GreetMessages = new string[] { "The Orc Leader sharpens its waraxe out its sharp blade" },
                DeathMessages = new string[] { "The Orc leader cries in agony as you land your killing blow" },
                Attack = Dice.Roll("3D5") + level / 3,
                AttackChance = Dice.Roll("15D4"),
                Awareness = 8,
                Color = Colors.OrcColor,
                Defense = Dice.Roll("1D3") + level / 3,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("5D4"),
                Health = health,
                MaxHealth = health,
                Name = "Orc Leader",
                Speed = 10,
                Experience = Dice.Roll("5D4") + level / 2,
                IsABoss = true,
                Symbol = 'O'
            };
        }
    }
}
