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
                itemPool.Add(new FoodRation(1),      12);
                itemPool.Add(new FoodRation(2),       4);
                itemPool.Add(new HealingPotion(),    15);
                itemPool.Add(new ManaPotion(),        3);
                itemPool.Add(new ToughnessPotion(),  13);
                itemPool.Add(new RevealMapScroll(),   5);
                itemPool.Add(new TeleportScroll(),    7);
                itemPool.Add(new BookOfWhirlwind(1),  4);
                itemPool.Add(new BookOfHealing(1),    4);
                itemPool.Add(new BookOfWhirlwind(2),  1);
                itemPool.Add(new PoisonFlask(1),      4);
                itemPool.Add(new ExplosiveFlask(1),   4);
                itemPool.Add(new SerpentWand(1),      4);
                itemPool.Add(new VampiricWand(1),     4);
                itemPool.Add(new PoisonFlask(2),      4);
                itemPool.Add(new ExplosiveFlask(2),   4);
                itemPool.Add(new SerpentWand(2),      4);
                itemPool.Add(new VampiricWand(2),     4);
            }
            else if (level <= 6)
            {
                itemPool.Add(new FoodRation(1),       3);
                itemPool.Add(new FoodRation(2),       5);
                itemPool.Add(new FoodRation(3),       2);
                itemPool.Add(new HealingPotion(),    14);
                itemPool.Add(new ManaPotion(),       14);
                itemPool.Add(new ToughnessPotion(),  13);
                itemPool.Add(new RevealMapScroll(),   4);
                itemPool.Add(new TeleportScroll(),    5);
                itemPool.Add(new BookOfWhirlwind(2),  4);
                itemPool.Add(new BookOfHealing(2),    4);
                itemPool.Add(new BookOfWhirlwind(3),  3);
                itemPool.Add(new PoisonFlask(1),      4);
                itemPool.Add(new ExplosiveFlask(1),   4);
                itemPool.Add(new SerpentWand(1),      4);
                itemPool.Add(new VampiricWand(1),     3);
                itemPool.Add(new SerpentWand(2),      4);
                itemPool.Add(new VampiricWand(2),     3);
                itemPool.Add(new PoisonFlask(2),      3);
                itemPool.Add(new ExplosiveFlask(2),   3);
                itemPool.Add(new DestructionWand(),   1);
            }
            else
            {
                itemPool.Add(new FoodRation(2),      10);
                itemPool.Add(new FoodRation(3),      10);
                itemPool.Add(new HealingPotion(),    13);
                itemPool.Add(new ManaPotion(),       15);
                itemPool.Add(new ToughnessPotion(),  13);
                itemPool.Add(new RevealMapScroll(),   5);
                itemPool.Add(new TeleportScroll(),    5);
                itemPool.Add(new BookOfWhirlwind(4),  5);
                itemPool.Add(new BookOfHealing(4),    5);
                itemPool.Add(new SerpentWand(2),      2);
                itemPool.Add(new VampiricWand(2),     2);
                itemPool.Add(new PoisonFlask(2),      2);
                itemPool.Add(new ExplosiveFlask(2),   2);
                itemPool.Add(new SerpentWand(3),      2);
                itemPool.Add(new VampiricWand(3),     2);
                itemPool.Add(new PoisonFlask(3),      2);
                itemPool.Add(new ExplosiveFlask(3),   2);
                itemPool.Add(new DestructionWand(),   1);
                itemPool.Add(new WeaponScroll(),      1);
                itemPool.Add(new ArmorScroll(),       1);
            }

            return itemPool.Get();
        }
    }
}
