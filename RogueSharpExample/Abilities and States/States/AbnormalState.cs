using System;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class AbnormalState : Ability
    {
        Player player = Game.Player;
        readonly string _status;
        readonly string _statusMessage;
        readonly int _adjustedAttack;
        readonly int _adjustedDefense;
        readonly int _adjustedAwareness;
        readonly int _adjustedSpeed;
        readonly int _poisonDamage;

        public AbnormalState(int turnsToRefresh, string status, string statusMessage, int adjustedAttack, int adjustedDefense, int adjustedAwareness, int adjustedSpeed, int poisonDamage)
        {
            TurnsToRefresh = turnsToRefresh;
            TurnsUntilRefreshed = 0;
            _status = status;
            _statusMessage = statusMessage;
            _adjustedAttack = adjustedAttack;
            _adjustedDefense = adjustedDefense;
            _adjustedAwareness = adjustedAwareness;
            _adjustedSpeed = adjustedSpeed;
            _poisonDamage = poisonDamage;
        }

        protected override bool PerformAbility()
        {
            if (player.Status != _status)
            {
                player.Status = _status;
                if(_statusMessage != "")
                {
                    Game.MessageLog.Add(_statusMessage, Swatch.DbBlood);
                }
                player.AdjustedAttack = _adjustedAttack;
                player.AdjustedDefense = _adjustedDefense;
                player.AdjustedAwareness = _adjustedAwareness;
                player.AdjustedSpeed = _adjustedSpeed;
                player.PoisonDamage = _poisonDamage;
            }
            else
            {
                player.AdjustedAttack = 0;
                player.AdjustedDefense = 0;
                player.AdjustedAwareness = 0;
                player.AdjustedSpeed = 0;
                player.Status = "Healthy";
                player.State = new DoNothing();
            }
            return true;
        }
    }
}
