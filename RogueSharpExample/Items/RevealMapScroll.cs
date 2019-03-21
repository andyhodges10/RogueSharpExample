using RogueSharp;
using RogueSharpExample.Core;

namespace RogueSharpExample.Items
{
    public class RevealMapScroll : Item
    {
        public RevealMapScroll()
        {
            Name = "Magic Map";
            RemainingUses = 1;
            Value = 35;
            Symbol = '?';
        }

        protected override bool UseItem()
        {
            DungeonMap map = Game.DungeonMap;
            Game.MessageLog.Add($"You read the {Name} and gains knowledge of the surrounding area");

            foreach (ICell cell in map.GetAllCells())
            {
                if (cell.IsWalkable)
                {
                    map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }

            RemainingUses--;
            return true;
        }
    }
}
