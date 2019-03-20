using RLNET;
using RogueSharpExample.Abilities;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class BookOfWhirlwind : Item
    {
        private readonly int _rarity;

        public BookOfWhirlwind(int rarity)
        {
            _rarity = rarity;
            if (_rarity == 1)
            {
                Name = "Book Of Whirlwind";
                Value = 100;
            }
            else if (_rarity == 2)
            {
                Name = "Book Of Poison Whirlwind";
                Value = 150;
            }
            else if (_rarity == 3)
            {
                Name = "Book Of Greater Whirlwind";
                Value = 200;
            }
            else
            {
                Name = "Book Of Greater Poison Whirlwind";
                Value = 300;
            }
            Color = RLColor.Yellow;
            RemainingUses = 1;
            Symbol = '+';
        }

        protected override bool UseItem()
        {
            Player player = Game.Player;
            
            Game.MessageLog.Add($"You read the {Name} and learn a new ability", Colors.Healing);

            LearnWhirlwind learnWhirlwind;
            if (_rarity == 1)
            {
                learnWhirlwind = new LearnWhirlwind(1, 2, 1, 3, 6, 2, "Whirlwind");
            }
            else if (_rarity == 2)
            {
                learnWhirlwind = new LearnWhirlwind(1, 2, 2, 40, 6, 2, "Whirlwind 2");
            }
            else if (_rarity == 2)
            {
                learnWhirlwind = new LearnWhirlwind(2, 3, 4, 10, 6, 2, "Whirlwind 3");
            }
            else
            {
                learnWhirlwind = new LearnWhirlwind(2, 2, 5, 40, 6, 2, "P.Whirlwind 2");
            }
            RemainingUses--;

            return learnWhirlwind.Perform();
        }
    }
}
