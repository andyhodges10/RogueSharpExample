using System;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class Meditate : Ability
    {
        private readonly int _amountToRegen;
        private readonly int _healthCost;

        public Meditate(int amountToRegen, int turnsToRefresh, int healthCost, string name)
        {
            
            TurnsToRefresh = turnsToRefresh;
            TurnsUntilRefreshed = 0;
            _amountToRegen = amountToRegen;
            _healthCost = healthCost;
            Name = name;
        }

        protected override bool PerformAbility()
        {
            Player player = Game.Player;

            if (player.Health < _healthCost)
            {
                Game.MessageLog.Add($"You don't have enough health to use this skill. It costs {_healthCost} health points", Swatch.DbBlood);

                return false;
            }
            else
            {
                player.Health -= _healthCost;
                player.Mana = Math.Min(player.MaxMana, player.Mana + _amountToRegen);

                return true;
            }
        }
    }
}
