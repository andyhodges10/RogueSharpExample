using RLNET;
using RogueSharp;
using RogueSharpExample.Interfaces;

namespace RogueSharpExample.Core
{
    public abstract class Plant : IDrawable
    {
        public RLColor Color { get; set; }
        public string Name { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public abstract void Draw(RLConsole console, IMap map);
    }
}
