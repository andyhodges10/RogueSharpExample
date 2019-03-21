using RLNET;
using RogueSharp.DiceNotation;
using RogueSharpExample.Abilities;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class ManaPotion : Item
    {
        public ManaPotion()
        {
            Name = "Mana Potion";
            Color = RLColor.LightBlue;
            RemainingUses = 0;
            Symbol = '!';
        }

        protected override bool UseItem()
        {
            Player player = Game.Player;
            if (player.Mana == player.MaxMana)
            {
                Game.MessageLog.Add("Your stumble around and waste an entire turn as your mana is already at max-out");
                return true;
            }
            else
            {
                int regenAmount = Dice.Roll("3D3") + 3;
                Game.MessageLog.Add($"You consume a {Name} and regens {regenAmount} mana", Colors.Healing);
                RegainMP regainMP = new RegainMP(regenAmount, 0);
                RemainingUses--;

                return regainMP.Perform();
            }
        }
    }
}
