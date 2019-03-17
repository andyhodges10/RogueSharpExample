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

            if (map.IsInFov(X, Y))
            {
                Color = Colors.TreeFov;
            }
            else
            {
                Color = Colors.Tree;
            }

            console.Set(X, Y, Color, null, Symbol);
        }
    }
}
