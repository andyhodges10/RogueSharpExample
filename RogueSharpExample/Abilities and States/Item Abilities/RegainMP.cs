using System;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class RegainMP : Ability
    {
        private readonly int _amountToRegain;

        public RegainMP(int amountToRegain, int turnsToRefresh)
        {
            TurnsToRefresh = turnsToRefresh;
            TurnsUntilRefreshed = 0;
            _amountToRegain = amountToRegain;
        }

        protected override bool PerformAbility()
        {
            Player player = Game.Player;
            player.Mana = Math.Min(player.MaxMana, player.Mana + _amountToRegain);

            return true;
        }
    }
}
