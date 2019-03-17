using System;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class Heal : Ability
    {
        private readonly int _amountToHeal;
        private readonly int _manaCost;

        public Heal(int amountToHeal, int turnsToRefresh, int manaCost, string name)
        {
            TurnsToRefresh = turnsToRefresh;
            TurnsUntilRefreshed = 0;
            _amountToHeal = amountToHeal;
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
                player.Health = Math.Min(player.MaxHealth, player.Health + _amountToHeal);

                return true;
            }
        }
    }
}
