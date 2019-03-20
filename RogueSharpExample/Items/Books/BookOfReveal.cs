using RLNET;
using RogueSharpExample.Abilities;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class BookOfReveal : Item // currently unused
    {
        private readonly int _rarity;
        public BookOfReveal(int rarity)
        {
            _rarity = rarity;
            if (_rarity == 1)
            {
                Name = "Book Of Minor Revealing";
                Value = 100;
            }
            else if (_rarity == 2)
            {
                Name = "Book Of Revealing";
                Value = 150;
            }
            else if (_rarity == 3)
            {
                Name = "Book Of Greater Revealing";
                Value = 200;
            }
            else
            {
                Name = "Book Of Epic Revealing";
                Value = 300;
            }
            Color = RLColor.Yellow;
            RemainingUses = 1;
            Symbol = '+';
        }
    }
}
