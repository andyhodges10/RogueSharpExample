using RogueSharpExample.Abilities;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class VampiricWand : Item
    {
        private readonly int _rarity;
        private readonly MagicMissile magicMissile;

        public VampiricWand(int rarity)
        {
            _rarity = rarity;
            if (_rarity == 1)
            {
                Name = "Low Quality Wand of Vampiric Strike";
                Value = 30;
                magicMissile = new MagicMissile(6, 90, 0, 0, 90, 5, 3, true, "Vampiric Strike");
            }
            else if (_rarity == 2)
            {
                Name = "Wand of Vampiric Strike";
                Value = 50;
                magicMissile = new MagicMissile(9, 95, 0, 0, 95, 6, 3, true, "Vampiric Strike");
            }
            else
            {
                Name = "High Quality Wand of Vampiric Strike";
                Value = 100;
                magicMissile = new MagicMissile(14, 100, 0, 0, 0, 0, 0, true, "Vampiric Strike");
            }

            RemainingUses = 3;
            Symbol = '/';
        }

        protected override bool UseItem()
        {
            Game.MessageLog.Add($"You use the {Name} and the skull starts to glow red");
            RemainingUses--;

            return magicMissile.Perform();
        }
    }
}
