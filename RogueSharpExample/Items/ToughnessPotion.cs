using RLNET;
using RogueSharp.DiceNotation;
using RogueSharpExample.Abilities;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class ToughnessPotion : Item
    {
        public ToughnessPotion()
        {
            Name = "Toughness Potion";
            Color = Swatch.DbBrightWood;
            RemainingUses = 0;
        }

        protected override bool UseItem()
        {

            Player player = Game.Player;
            if (player.Status == "Hardened")
            {
                Game.MessageLog.Add("You decide not to chug it down as you are already taking advantage of its effects");
                return true;
            }
            else
            {
                Game.MessageLog.Add($"You consume a {Name}", Colors.Healing);
                Hardened hardened = new Hardened(12, 0, "Hardened");
                RemainingUses--;

                
                return hardened.Perform();
            }
        }
    }
}
