using RLNET;
using RogueSharp.DiceNotation;
using RogueSharpExample.Abilities;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class HealingPotion : Item
    {
        public HealingPotion()
        {
            Name = "Healing Potion";
            Color = RLColor.LightRed;
            RemainingUses = 0;
        }

        protected override bool UseItem()
        {
            Player player = Game.Player;
            if (player.Health == player.MaxHealth)
            {
                Game.MessageLog.Add("Your stumble around and waste an entire turn as your health is already at max-out");
                return true;
            }
            else
            {
                int healAmount = Dice.Roll("4D3") + 4; // improved: Dice.Roll("5D4") + 5;
                Game.MessageLog.Add($"You consume a {Name} and recovers {healAmount} health", Colors.Healing);
                RegainHP regainHP = new RegainHP(healAmount, 0);
                RemainingUses--;

                return regainHP.Perform();
            }
        }
    }
}
