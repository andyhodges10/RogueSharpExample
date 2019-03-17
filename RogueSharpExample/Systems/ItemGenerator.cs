using RogueSharpExample.Core;
using RogueSharpExample.Items;

namespace RogueSharpExample.Systems
{
    public static class ItemGenerator
    {
        public static Item CreateItem(int level)
        {
            Pool<Item> itemPool = new Pool<Item>();
            
            if (level <= 3)
            {
                // itemPool.Add(new ArmorScroll(), 10);
                // itemPool.Add(new Whetstone(), 10);
                itemPool.Add(new FoodRation(), 40);
                itemPool.Add(new HealingPotion(), 25);
                itemPool.Add(new ToughnessPotion(), 20);
                itemPool.Add(new RevealMapScroll(), 5);
                itemPool.Add(new TeleportScroll(), 10);
            }
            else if (level <= 6)
            {
                itemPool.Add(new DestructionWand(), 1);
                itemPool.Add(new FoodRation(), 20);
                itemPool.Add(new HealingPotion(), 25);
                itemPool.Add(new ManaPotion(), 25);
                itemPool.Add(new ToughnessPotion(), 20);
                itemPool.Add(new RevealMapScroll(), 4);
                itemPool.Add(new TeleportScroll(), 5);
            }
            else
            {
                itemPool.Add(new DestructionWand(), 2);
                itemPool.Add(new FoodRation(), 15);
                itemPool.Add(new HealingPotion(), 25);
                itemPool.Add(new ManaPotion(), 25);
                itemPool.Add(new ToughnessPotion(), 20);
                itemPool.Add(new RevealMapScroll(), 5);
                itemPool.Add(new TeleportScroll(), 8);
            }

            return itemPool.Get();
        }
    }
}
