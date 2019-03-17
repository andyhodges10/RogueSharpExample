using RogueSharp;
using RogueSharpExample.Core;
using RogueSharpExample.Interfaces;

namespace RogueSharpExample.Abilities
{
    public class Fireball : Ability, ITargetable
    {
        private readonly int _attack;
        private readonly int _attackChance;
        private readonly int _area;
        private readonly int _manaCost;

        public Fireball( int attack, int attackChance, int area, int turnsToRefresh, int manaCost, string name)
        {
            _attack = attack;
            _attackChance = attackChance;
            _area = area;
            TurnsToRefresh = turnsToRefresh;
            TurnsUntilRefreshed = 0;
            _manaCost = manaCost;
            Name = name;
        }

        protected override bool PerformAbility()
        {
            Player player = Game.Player;
            if (player.Mana < _manaCost)
            {
                Game.MessageLog.Add($"You don't have enough mana to use this skill. It costs {_manaCost} mana points", Swatch.DbBlood);

                return false;
            }
            else
            {
                player.Mana -= _manaCost;
                return Game.TargetingSystem.SelectArea(this, _area);
            }
        }

        public void SelectTarget( Point target )
        {
            DungeonMap dungeonmap = Game.DungeonMap;
            Player player = Game.Player;
            Game.MessageLog.Add( $"{player.Name} casts a {Name}" );
            Actor fireballActor = new Actor { Attack = _attack, AttackChance = _attackChance, Name = Name };
            foreach (ICell cell in dungeonmap.GetCellsInCircle(target.X, target.Y, _area))
            {
                Monster monster = dungeonmap.GetMonsterAt(cell.X, cell.Y);
                if (monster != null)
                {
                    Game.CommandSystem.Attack(fireballActor, monster);
                }

                if (dungeonmap.CheckForPlayer(cell.X, cell.Y))
                {
                    Game.CommandSystem.Attack(fireballActor, player);
                }
            }
        }
    }
}
