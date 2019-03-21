using System.Collections.Generic;
using RogueSharp;
using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class Whirlwind : Ability
    {
        private readonly int _range;
        private readonly int _manaCost;
        private readonly int _poisonChance;
        private readonly int _poisonLength;
        private readonly int _poisonDamage;

        public Whirlwind(int range, int turnsToRefresh, int manaCost, int poisonChance, int poisonLength, int poisonDamage, string name)
        {
            _range = range;
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
            DungeonMap map = Game.DungeonMap;
            Player player = Game.Player;

            if (player.Mana < _manaCost)
            {
                Game.MessageLog.Add($"You don't have enough mana to use this skill", Swatch.DbBlood);

                return false;
            }
            else
            {
                player.Mana -= _manaCost;
                Game.MessageLog.Add($"You perform a whirlwind attack against all enemies {_range} tile(s) away");
                List <Point> monsterLocations = new List<Point>();

                foreach (ICell cell in map.GetCellsInSquare(player.X, player.Y, _range))
                {
                    foreach (Point monsterLocation in map.GetMonsterLocations())
                    {
                        if (cell.X == monsterLocation.X && cell.Y == monsterLocation.Y)
                        {
                            monsterLocations.Add(monsterLocation);
                        }
                    }
                }

                foreach (Point monsterLocation in monsterLocations)
                {
                    Monster monster = map.GetMonsterAt(monsterLocation.X, monsterLocation.Y);
                    if (monster != null)
                    {
                        Game.CommandSystem.Attack(player, monster);
                        if (Dice.Roll("1D100") < _poisonChance)
                        {
                            if (monster.IsPoisonedImmune == false && _poisonDamage != 0)
                            {
                                monster.State = new MonsterAbnormalState(monster, _poisonLength, "Poisoned", -2, -2, _poisonDamage);
                            }
                        }
                    }
                }

                return true;
            }
        }
    }
}
