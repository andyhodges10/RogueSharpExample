using RLNET;
using RogueSharp;
using RogueSharpExample.Interfaces;

namespace RogueSharpExample.Core
{
    public class Item : IItem, ITreasure, IDrawable
    {
        public Item()
        {
            Symbol = '!';
            Color = RLColor.Yellow;
        }

        public string Name { get; set; }
        public string Name2 { get; set; } 
        public int Value { get; set; }
        public int RemainingUses { get; set; }

        public bool Use()
        {
            return UseItem();
        }

        protected virtual bool UseItem()
        {
            return false;
        }

        public bool PickUp(IActor actor)
        {
            if (actor is Player player)
            {
                if (player.AddItem(this))
                {
                    Game.MessageLog.Add($"You picked up {Name}");
                    return true;
                }
            }

            return false;
        }

        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.IsExplored(X, Y))
            {
                return;
            }

            if (map.IsInFov(X, Y))
            {
                console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            }
            else
            {
                console.Set(X, Y, RLColor.Blend(Color, RLColor.Gray, 0.5f), Colors.Background, Symbol);
            }
        }
    }
}
