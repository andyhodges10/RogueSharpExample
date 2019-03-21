using RogueSharpExample.Core;
using RogueSharpExample.Equipment;

namespace RogueSharpExample.Items
{
    public class ArmorScroll : Item
    {
        public ArmorScroll()
        {
            Name = "Enhance Armor Scroll";
            RemainingUses = 1;
            Value = 200;
            Symbol = '%';
        }

        protected override bool UseItem()
        {
            Player player = Game.Player;

            if (player.Body == BodyEquipment.None())
            {
                Game.MessageLog.Add($"You are not wearing any body armor to enhance");
            }
            else if (player.Body.Defense >= 4)
            {
                Game.MessageLog.Add($"You cannot enhance your {player.Body.Name} any more");
            }
            else
            {
                Game.MessageLog.Add($"You use a {Name} to enhance their {player.Body.Name}");
                player.Body.Defense += 1;
                RemainingUses--;
            }

            return true;
        }
    }
}
