using RogueSharp;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class RevealMap : Ability
    {
        private readonly int _revealDistance;
        private readonly int _manaCost;

        public RevealMap( int revealDistance, int turnsToRefresh, int manaCost, string name)
        {
            TurnsToRefresh = turnsToRefresh;
            TurnsUntilRefreshed = 0;
            _revealDistance = revealDistance;
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

                foreach (ICell cell in map.GetCellsInCircle(player.X, player.Y, _revealDistance))
                {
                    if (cell.IsWalkable)
                    {
                        map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                    }
                }

                return true;
            }
        }
    }
}
