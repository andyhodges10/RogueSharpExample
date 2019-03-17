using RLNET;
using RogueSharp;

namespace RogueSharpExample.Core
{
    public class Grass : Plant
    {
        public Grass(int x, int y)
        {
            Name = "grass";
            Symbol = ',';
            X = x;
            Y = y;
        }

        /// <summary>
        /// Will draw grass.
        /// </summary>
        /// <param name="console"></param>
        /// <param name="map"></param>
        public override void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(X, Y).IsExplored)
            {
                return;
            }

            if (map.IsInFov(X, Y))
            {
                Color = Swatch.DbGrass;
            }
            else
            {
                Color = Swatch.DbOldStone;
            }

            console.Set(X, Y, Color, null, Symbol);
        }
    }
}
