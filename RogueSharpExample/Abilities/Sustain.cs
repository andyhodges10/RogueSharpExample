using System;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class Sustain : Ability
    {
        private readonly int _amountToSustain;
        private readonly int _healthCost;

        public Sustain(int amountToSustain, int turnsToRefresh, int healthCost, string name)
        {
            _amountToSustain = amountToSustain;
            TurnsToRefresh = turnsToRefresh;
            TurnsUntilRefreshed = 0;
            _healthCost = healthCost;
            Name = name;
        }

        protected override bool PerformAbility()
        {
            Player player = Game.Player;
            if (_amountToSustain + player.Hunger > player.MaxHunger)
            {
                Game.MessageLog.Add($"You don't have enough health to use this skill. It costs {_healthCost} health points", Swatch.DbBlood);

                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
