using RogueSharp;
using RogueSharp.DiceNotation;
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
        private readonly int _poisonChance;
        private readonly int _poisonLength;
        private readonly int _poisonDamage;

        public Fireball( int attack, int attackChance, int area, int turnsToRefresh, int manaCost, int poisonChance, int poisonLength, int poisonDamage, string name)
        {
            _attack = attack;
            _attackChance = attackChance;
            _area = area;
            TurnsToRefresh = turnsToRefresh;
            TurnsUntilRefreshed = 0;
            _manaCost = manaCost;
            _poisonChance = poisonChance;
            _poisonLength = poisonLength;
            _poisonDamage = poisonDamage;
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
                    if (Dice.Roll("1D100") < _poisonChance)
                    {
                        if (monster.IsPoisonedImmune == false && _poisonDamage != 0)
                        {
                            monster.State = new MonsterAbnormalState(monster, _poisonLength, "Poisoned", -1, -1, _poisonDamage);
                        }
                    }
                }

                if (dungeonmap.CheckForPlayer(cell.X, cell.Y))
                {
                    Game.CommandSystem.Attack(fireballActor, player);
                    if (Dice.Roll("1D100") < _poisonChance)
                    {
                        if (player.IsPoisonedImmune == false && _poisonDamage != 0)
                        {
                            player.State = new AbnormalState(_poisonLength, "Poisoned", -1, -1, _poisonDamage);
                        }
                    }
                }
            }
        }
    }
}
