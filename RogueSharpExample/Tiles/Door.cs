using RLNET;
using RogueSharp;
using RogueSharpExample.Interfaces;

namespace RogueSharpExample.Core
{
    public class Door : IDrawable
    {
        public Door()
        {
            Symbol = '+';
            Color = Colors.Door;
        }
        public bool IsOpen { get; set; }

        public RLColor Color { get; set; }
        public RLColor BackgroundColor { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(X, Y).IsExplored)
            {
                return;
            }
            Symbol = IsOpen ? '-' : '+';

            double distance = Game.DistanceBetween(Game.Player.X, Game.Player.Y, X, Y);
            float blendRatio = .5f / Game.Player.Awareness;
            float blendAmount = (float)(blendRatio * distance);

            if (map.IsInFov(X, Y))
            {
                console.Set(X, Y, RLColor.Blend(Colors.DoorFov, Colors.Door, .5f - blendAmount), null, Symbol);
            }
            else
            {
                console.Set(X, Y, Colors.Door, null, Symbol);
            }
        }
    }
}
