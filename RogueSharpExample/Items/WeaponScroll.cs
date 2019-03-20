using RogueSharpExample.Core;
using RogueSharpExample.Equipment;

namespace RogueSharpExample.Items
{
    public class WeaponScroll : Item
    {
        public WeaponScroll()
        {
            Name = "Enhance Weapon Scroll";
            RemainingUses = 1;
            Symbol = '?';
        }

        protected override bool UseItem()
        {
            Player player = Game.Player;

            if (player.Hand == HandEquipment.None())
            {
                Game.MessageLog.Add($"You are not wielding any weapon to enhance");
            }
            else if (player.Hand.Attack >= 5)
            {
                Game.MessageLog.Add($"You cannot enhance your {player.Hand.Name} any more");
            }
            else
            {
                Game.MessageLog.Add($"You use a {Name} to enhance their {player.Hand.Name}");
                player.Hand.Attack += 1;
                RemainingUses--;
            }

            return true;
        }
    }
}
