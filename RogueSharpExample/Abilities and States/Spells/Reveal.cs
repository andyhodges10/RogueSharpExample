using RogueSharp;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    class Reveal : Ability
    {
        private readonly int _manaCost;

        public Reveal(int turnsToRefresh, int manaCost, string name)
        {
            TurnsToRefresh = turnsToRefresh;
            TurnsUntilRefreshed = 0;
            _manaCost = manaCost;
            Name = name;
        }

        protected override bool PerformAbility()
        {
            DungeonMap map = Game.DungeonMap;
            Player player = Game.Player;
            if (player.Mana < _manaCost)
            {
                Game.MessageLog.Add($"You don't have enough mana to use this skill. It costs {_manaCost} mana points", Swatch.DbBlood);

                return false;
            }
            else
            {
                player.Mana -= _manaCost;
                
                foreach (ICell cell in map.GetAllCells())
                {
                    if (cell.IsWalkable)
                    {
                        map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                    }
                }
                Game.MessageLog.Add($"You cast {Name} and gains knowledge of the surrounding area");

                return true;
            }
        }
    }
}
