using System;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class RegainHP : Ability
    {
        private readonly int _amountToRegain;

        public RegainHP(int amountToRegain, int turnsToRefresh)
        {
            TurnsToRefresh = turnsToRefresh;
            TurnsUntilRefreshed = 0;
            _amountToRegain = amountToRegain;
        }

        protected override bool PerformAbility()
        {
            Player player = Game.Player;
            player.Health = Math.Min(player.MaxHealth, player.Health + _amountToRegain);

            return true;
        }
    }
}
