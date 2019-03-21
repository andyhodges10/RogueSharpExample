using System.Collections.Generic;
using System.Linq;
using RogueSharpExample.Interfaces;

namespace RogueSharpExample.Core
{
    public class Inventory
    {
        public List<ITreasure> Item { get; protected set; }
        readonly Actor _owner;

        public Inventory(Actor owner)
        {
            Item = new List<ITreasure>();
            _owner = owner;
        }

        public void AddInventoryItem(Item inventoryItem)
        {
            Item.Add(inventoryItem);
        }

        public bool UseItemInSlot(char slot)
        {
            int index = slot - 97;

            if (index > Item.Count())
            {
                return false;
            }

            try {
                Item i = Item.ElementAt(index) as Item;
                i.Use();
                if (i.RemainingUses <= 0)
                {
                    Item.RemoveAt(index);
                }

                return true;
            }
            catch (System.ArgumentOutOfRangeException) {
                return false;
            }
        }

        public bool PurchaseItemInSlot(char slot)
        {
            int index = slot - 97;

            if (index > Item.Count())
            {
                return false;
            }

            try
            {
                Item i = Item.ElementAt(index) as Item;
                if (i.Value * 3 < Game.Player.Gold)
                {
                    Game.Player.Gold -= i.Value * 3;
                    Game.MessageLog.Add($"You purchased the {i.Name} for {i.Value * 3} gold pieces");
                    Game.Player.Inventory.AddInventoryItem(i);
                    Item.RemoveAt(index);

                    return true;
                }
                else
                {
                    Game.MessageLog.Add($"You are too broke to afford the {i.Name} it costs {i.Value * 3} gold pieces");

                    return true;
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        public bool SellItemInSlot(char slot)
        {
            int index = slot - 97;

            if (index > Item.Count())
            {
                return false;
            }

            try {
                Item i = Item.ElementAt(index) as Item;
                Game.Player.Gold += i.Value;
                Game.MessageLog.Add($"You sold the {i.Name} for {i.Value} gold pieces");
                Item.RemoveAt(index);

                return true;
            }
            catch (System.ArgumentOutOfRangeException) {
                return false;
            }
        }
    }
}
