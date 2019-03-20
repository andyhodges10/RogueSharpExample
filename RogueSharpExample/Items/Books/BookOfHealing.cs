using RLNET;
using RogueSharpExample.Abilities;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class BookOfHealing : Item
    {
        private readonly int _rarity;

        public BookOfHealing(int rarity)
        {
            _rarity = rarity;
            if (_rarity == 1)
            {
                Name = "Book Of Minor Healing";
                Value = 100;
            }
            else if (_rarity == 2)
            {
                Name = "Book Of Healing";
                Value = 150;
            }
            else if (_rarity == 3)
            {
                Name = "Book Of Greater Healing";
                Value = 200;
            }
            else
            {
                Name = "Book Of Epic Healing";
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

            LearnHeal LearnHeal;
            if (_rarity == 1)
            {
                LearnHeal = new LearnHeal(1, 20, 5, "Healing 1");
            }
            else if (_rarity == 2)
            {
                LearnHeal = new LearnHeal(1, 15, 8, "Healing 2");
            }
            else if (_rarity == 2)
            {
                LearnHeal = new LearnHeal(1, 12, 10, "Healing 3");
            }
            else
            {
                LearnHeal = new LearnHeal(1, 10, 12, "Healing 4");
            }
            RemainingUses--;

            return LearnHeal.Perform();
        }
    }
}
