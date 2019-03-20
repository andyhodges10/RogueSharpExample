using RLNET;
using RogueSharp;
using RogueSharpExample.Interfaces;

namespace RogueSharpExample.Core
{
    public class Tree : Plant
    {
        public Tree(int x, int y)
        {
            Name = "tree";
            Symbol = 'T';
            X = x;
            Y = y;
        }

        public override void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(X, Y).IsExplored)
            {
                return;
            }

            double distance = Game.DistanceBetween(Game.Player.X, Game.Player.Y, X, Y);
            float blendRatio = .5f / Game.Player.Awareness;
            float blendAmount = (float)(blendRatio * distance);

            if (map.IsInFov(X, Y))
            {
                console.Set(X, Y, RLColor.Blend(Colors.LowLevelFloorFov, Colors.LowLevelFloor, .5f - blendAmount), null, Symbol);
            }
            else
            {
                console.Set(X, Y, Colors.LowLevelFloor, null, Symbol);
            }
        }
    }
}
