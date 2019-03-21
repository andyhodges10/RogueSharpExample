using RLNET;
using RogueSharpExample.Abilities;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class BookOfSacrifice : Item
    {
        private readonly int _rarity;

        public BookOfSacrifice(int rarity)
        {
            _rarity = rarity;
            if (_rarity == 1)
            {
                Name = "Book Of Minor Sacrifice";
                Value = 100;
            }
            else if (_rarity == 2)
            {
                Name = "Book Of Sacrifice";
                Value = 150;
            }
            else if (_rarity == 3)
            {
                Name = "Book Of Greater Sacrifice";
                Value = 200;
            }
            else
            {
                Name = "Book Of Epic Sacrifice";
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

            LearnSacrifice LearnSacrifice;
            if (_rarity == 1)
            {
                LearnSacrifice = new LearnSacrifice(1, 20, 5, "Sacrifice 1");
            }
            else if (_rarity == 2)
            {
                LearnSacrifice = new LearnSacrifice(2, 15, 8, "Sacrifice 2");
            }
            else if (_rarity == 2)
            {
                LearnSacrifice = new LearnSacrifice(3, 12, 10, "Sacrifice 3");
            }
            else
            {
                LearnSacrifice = new LearnSacrifice(4, 10, 12, "Sacrifice 4");
            }
            RemainingUses--;

            return LearnSacrifice.Perform();
        }
    }
}
