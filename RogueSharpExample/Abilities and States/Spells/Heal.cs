using System;
using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class Heal : Ability
    {
        private readonly int _level;
        private readonly int _manaCost;
        private int healAmount;

        public Heal(int level, int turnsToRefresh, int manaCost, string name)
        {
            _level = level;
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
            else if (player.Health == player.MaxHealth)
            {
                Game.MessageLog.Add("You decide to not cast the spell as your health is already at max-out");

                return false;
            }
            else
            {
                player.Mana -= _manaCost;

                if(_level == 1)
                {
                    healAmount = Dice.Roll("3D3") + 1;
                }
                else if (_level == 2)
                {
                    healAmount = Dice.Roll("4D3") + 3;
                }
                else if (_level == 3)
                {
                    healAmount = Dice.Roll("5D4") + 4;
                }
                else
                {
                    healAmount = Dice.Roll("6D5") + 5;
                }

                Game.MessageLog.Add($"You heal yourself for {healAmount} health points");
                player.Health = Math.Min(player.MaxHealth, player.Health + healAmount);

                return true;
            }
        }
    }
}
