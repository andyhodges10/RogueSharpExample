using RogueSharpExample.Abilities;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class SerpentWand : Item
    {
        private readonly int _rarity;
        private readonly MagicMissile magicMissile;

        public SerpentWand(int rarity)
        {
            _rarity = rarity;
            if (_rarity == 1)
            {
                Name = "Low Quality Wand of Poison Strike";
                Value = 30;
                magicMissile = new MagicMissile(3, 70, 0, 0, 90, 5, 3, false, "Poison Strike");
            }
            else if (_rarity == 2)
            {
                Name = "Wand of Poison Strike";
                Value = 50;
                magicMissile = new MagicMissile(4, 75, 0, 0, 95, 6, 3, false, "Poison Strike");
            }
            else
            {
                Name = "High Quality Wand of Poison Strike";
                Value = 100;
                magicMissile = new MagicMissile(6, 85, 0, 0, 100, 6, 4, false, "Poison Strike");
            }

            RemainingUses = 3;
            Symbol = '/';
        }

        protected override bool UseItem()
        {
            Game.MessageLog.Add($"You use the {Name} and the snake starts to glow green");
            RemainingUses--;
            
            return magicMissile.Perform();
        }
    }
}
