using RogueSharp;
using RogueSharp.DiceNotation;
using RogueSharpExample.Core;
using RogueSharpExample.Interfaces;

namespace RogueSharpExample.Abilities
{
    public class MagicMissile : Ability, ITargetable
    {
        private readonly int _attack;
        private readonly int _attackChance;
        private readonly int _manaCost;
        private readonly int _poisonChance;
        private readonly int _poisonLength;
        private readonly int _poisonDamage;
        private readonly bool _lifestealing;

        public MagicMissile(int attack, int attackChance, int turnsToRefresh, int manaCost, int poisonChance, int poisonLength, int poisonDamage, bool lifestealing, string name)
        {
            TurnsToRefresh = turnsToRefresh;
            TurnsUntilRefreshed = 0;
            _attack = attack;
            _attackChance = attackChance;
            _manaCost = manaCost;
            _poisonChance = poisonChance;
            _poisonLength = poisonLength;
            _poisonDamage = poisonDamage;
            _lifestealing = lifestealing;
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
                if (Dice.Roll("1D100") < _poisonChance)
                {
                    if (monster.IsPoisonedImmune == false && _poisonDamage != 0)
                    {
                        monster.State = new MonsterAbnormalState(monster, _poisonLength, "Poisoned", -1, -1, _poisonDamage);
                    }
                }
                if (_lifestealing == true)
                {
                    Game.MessageLog.Add($"You steal {_attack / 2} health points from the {monster.Name}", Colors.Healing);
                    RegainHP regainHP = new RegainHP(_attack / 2, 0);
                    regainHP.Perform();
                }
            }
        }
    }
}
