using System;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class RegainHunger : Ability
    {
        private readonly int _amountToRegain;

        public RegainHunger(int amountToRegain, int turnsToRefresh)
        {
            _amountToRegain = amountToRegain;
            TurnsToRefresh = turnsToRefresh;
            TurnsUntilRefreshed = 0;
        }

        protected override bool PerformAbility()
        {
            Player player = Game.Player;
            player.Hunger = Math.Min(player.MaxHunger, player.Hunger + _amountToRegain);

            return true;
        }
    }
}
