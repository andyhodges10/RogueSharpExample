using System;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class MonsterAbnormalState : Ability
    {
        Monster _monster;
        readonly string _status;
        readonly int _adjustedAttack;
        readonly int _adjustedDefense;
        readonly int _poisonDamage;

        public MonsterAbnormalState(Monster monster, int turnstoRefresh, string status, 
                                    int adjustedAttack, int adjustedDefense, int poisonDamage)
        {
            _monster = monster;
            _status = status;
            _adjustedAttack = adjustedAttack;
            _adjustedDefense = adjustedDefense;
            _poisonDamage = poisonDamage;
            TurnsToRefresh = turnstoRefresh;
            TurnsUntilRefreshed = 0;
        }

        protected override bool PerformAbility()
        {
            if (_monster.Status != _status)
            {
                _monster.Status = _status;
                _monster.AdjustedAttack = _adjustedAttack;
                _monster.AdjustedDefense = _adjustedDefense;
                _monster.PoisonDamage = _poisonDamage;
            }
            else
            {
                _monster.AdjustedAttack = 0;
                _monster.AdjustedDefense = 0;
                _monster.Status = "Healthy";
                _monster.State = new DoNothing();
            }
            return true;
        }
    }
}
