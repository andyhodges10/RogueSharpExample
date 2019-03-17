using RogueSharp;
using RogueSharpExample.Core;
using RogueSharpExample.Interfaces;

namespace RogueSharpExample.Abilities
{
    public class LightningBolt : Ability, ITargetable
    {
        private readonly int _attack;
        private readonly int _attackChance;
        private readonly int _manaCost;

        public LightningBolt(int attack, int attackChance, int turnstoRefresh, int manaCost, string name)
        {
            Name = name;
            TurnsToRefresh = turnstoRefresh;
            TurnsUntilRefreshed = 0;
            _attack = attack;
            _attackChance = attackChance;
            _manaCost = manaCost;
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
                return Game.TargetingSystem.SelectLine(this);
            }
        }

        public void SelectTarget( Point target )
        {
            DungeonMap map = Game.DungeonMap;
            Player player = Game.Player;
            Game.MessageLog.Add( $"{player.Name} casts a {Name}" );

            Actor lightningBoltActor = new Actor {
            Attack = _attack,
            AttackChance = _attackChance,
            Name = Name
            };
            foreach ( ICell cell in map.GetCellsAlongLine( player.X, player.Y, target.X, target.Y ) )
            {
                if ( cell.IsWalkable )
                {
                    continue;
                }

                if ( cell.X == player.X && cell.Y == player.Y )
                {
                    continue;
                }

                Monster monster = map.GetMonsterAt( cell.X, cell.Y );
                if ( monster != null )
                {
                    Game.CommandSystem.Attack( lightningBoltActor, monster );
                }
                else
                {
                    return;
                }
            }
        }
    }
}
