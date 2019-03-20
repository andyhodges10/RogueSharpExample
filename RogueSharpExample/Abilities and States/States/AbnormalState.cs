using System;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class AbnormalState : Ability
    {
        Player player = Game.Player;
        readonly string _status;
        readonly int _adjustedAttack;
        readonly int _adjustedDefense;
        readonly int _poisonDamage;

        public AbnormalState(int turnsToRefresh, string status, int adjustedAttack, int adjustedDefense, int poisonDamage)
        {
            TurnsToRefresh = turnsToRefresh;
            TurnsUntilRefreshed = 0;
            _status = status;
            _adjustedAttack = adjustedAttack;
            _adjustedDefense = adjustedDefense;
            _poisonDamage = poisonDamage;
        }

        protected override bool PerformAbility()
        {
            if (player.Status != _status)
            {
                player.Status = _status;
                player.AdjustedAttack = _adjustedAttack;
                player.AdjustedDefense = _adjustedDefense;
                player.PoisonDamage = _poisonDamage;
            }
            else
            {
                player.AdjustedAttack = 0;
                player.AdjustedDefense = 0;
                player.Status = "Healthy";
                player.State = new DoNothing();
            }
            return true;
        }
    }
}
