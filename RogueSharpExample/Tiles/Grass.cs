using System;
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

        private static int WalkingDistanceBetween( int x1, int y1, int x2, int y2 )
        {
           return Math.Abs( x2 - x1 ) + Math.Abs( y2 - y1 );
        }

        private static double DistanceBetween( int x1, int y1, int x2, int y2 )
        {
           return Math.Sqrt( Math.Pow( x2 - x1, 2 ) + Math.Pow( y2 - y1, 2 ) );
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
            double distance = DistanceBetween( Game.Player.X, Game.Player.Y, X, Y );
            float blendRatio = .5f / Game.Player.Awareness;
            float blendAmount = (float) ( blendRatio * distance );

            int walkingDistance = WalkingDistanceBetween( Game.Player.X, Game.Player.Y, X, Y );
            byte greenValue = (byte) (255 - (walkingDistance * 5));
            byte redValue = (byte) ( 150 - ( walkingDistance * 2 ) );
         
            if (map.IsInFov(X, Y))
            {
                RLColor shadedGrass = new RLColor( redValue, greenValue, 44 );
                console.Set(X, Y, shadedGrass, RLColor.Blend( RLColor.Black, Swatch.DbOldStone, .5f - blendAmount ), Symbol );
            }
            else
            {
                console.Set(X, Y, Swatch.DbOldStone, null, Symbol);
            }
        }
    }
}
