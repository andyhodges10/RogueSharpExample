using RLNET;
using RogueSharp;
using RogueSharpExample.Interfaces;

namespace RogueSharpExample.Core
{
    public class Trap : ITrap, ITreasure, IDrawable
    {
        public Trap()
        {
            Symbol = '^';
            Color = Colors.Trap;
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public bool Triggered()
        {
            return TrapTriggered();
        }

        protected virtual bool TrapTriggered()
        {
            return false;
        }

        public bool PickUp(IActor actor)
        {
            if (actor is Player player)
            {
                Triggered();
                return true;
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
