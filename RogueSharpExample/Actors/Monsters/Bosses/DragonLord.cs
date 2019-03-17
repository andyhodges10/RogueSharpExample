using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Monsters
{
    class DragonLord : Monster
    {
        public static DragonLord Create(int level)
        {
            int health = Dice.Roll("8D8");
            return new DragonLord {
                AttackMessages = new string[] { "The DragonLord thrusts its maw at you" },
                GreetMessages = new string[] { "The DragonLord stands talls and assumes its battle stance", "The DragonLord points at you and draws its Magical Sword" },
                DeathMessages = new string[] { "The DragonLord falls over dead. You win", "You finally have defated the DragonLord, your quest is complete" },
                Attack = Dice.Roll("5D4") + level / 3,
                AttackChance = Dice.Roll("15D5"),
                Awareness = 12,
                Color = Colors.DragonColor,
                Defense = Dice.Roll("1D6") + level / 3,
                DefenseChance = Dice.Roll("10D5"),
                Gold = Dice.Roll("6D6"),
                Health = health,
                MaxHealth = health,
                Name = "DragonLord",
                Speed = 10,
                Experience = Dice.Roll("6D5") + level / 2,
                IsABoss = true,
                IsEndBoss = true,
                Symbol = 'D'
            };
        }
    }
}
