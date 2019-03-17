using RogueSharp.DiceNotation;
using RogueSharpExample.Behaviors;
using RogueSharpExample.Core;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Monsters
{
    public class Viper : Monster
    {
        private bool _didPoison = false;

        public static Viper Create(int level)
        {
            int health = Dice.Roll("4D3");
            return new Viper {
                AttackMessages = new string[] { "The Viper goes in for a bite" },
                GreetMessages = new string[] { "The Viper hisses at you" },
                DeathMessages = new string[] { "You decapitate the Viper" },
                Attack = Dice.Roll("2D3") + level / 3,
                AttackChance = Dice.Roll("30D3"),
                Awareness = 10,
                Color = Colors.ViperColor,
                Defense = Dice.Roll("1D2") + level / 3,
                DefenseChance = Dice.Roll("10D3"),
                Gold = Dice.Roll("5D6"),
                Health = health,
                MaxHealth = health,
                Name = "Viper",
                Speed = 14,
                Experience = Dice.Roll("2D3") + level / 2,
                PoisonDamage = 3,
                IsPoisonedImmune = true,
                Symbol = 'V'
            };
        }

        public override void PerformAction(CommandSystem commandSystem)
        {
            var monsterPoisonBehavior = new MonsterPoison();

            if (Health < MaxHealth / 2 && _didPoison == false)
            {
                _didPoison = monsterPoisonBehavior.Act(this, commandSystem);
            }
            else
            {
                base.PerformAction(commandSystem);
            }
        }
    }
}
