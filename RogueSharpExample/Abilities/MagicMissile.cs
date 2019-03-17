using RogueSharp;
using RogueSharpExample.Core;
using RogueSharpExample.Interfaces;

namespace RogueSharpExample.Abilities
{
    public class MagicMissile : Ability, ITargetable
    {
        private readonly int _attack;
        private readonly int _attackChance;
        private readonly int _manaCost;

        public MagicMissile(int attack, int attackChance, int turnsToRefresh, int manaCost, string name)
        {
            Name = name;
            TurnsToRefresh = turnsToRefresh;
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
                return Game.TargetingSystem.SelectMonster(this);
            }
        }
        public void SelectTarget(Point target)
        {
            DungeonMap map = Game.DungeonMap;
            Player player = Game.Player;
            Monster monster = map.GetMonsterAt(target.X, target.Y);
            if (monster != null)
            {
                Game.MessageLog.Add($"{player.Name} casts a {Name} at {monster.Name}");
                Actor magicMissleActor = new Actor {
                    Attack = _attack, AttackChance = _attackChance, Name = Name
                };
                Game.CommandSystem.Attack(magicMissleActor, monster);
            }
        }
    }
}
