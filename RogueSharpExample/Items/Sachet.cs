using RLNET;
using RogueSharpExample.Abilities;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class Satchet : Item
    {
        public Satchet(int rarity)
        {
            if (rarity == 1)
            {
                Name = "Low Quality Satchet";
                Value = 10;
            }
            else if (rarity == 2)
            {
                Name = "Satchet";
                Value = 15;
            }
            else
            {
                Name = "High Quality Satchet";
                Value = 30;
            }
            Color = Swatch.DbBrightWood;
            RemainingUses = 1;
            Symbol = '!';
        }

        protected override bool UseItem() // dome
        {
            return true;
        }
    }
}
