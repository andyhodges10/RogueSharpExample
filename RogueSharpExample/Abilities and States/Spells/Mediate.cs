using System;
using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class Meditate : Ability
    {
        private readonly int _level;
        private readonly int _healthCost;
        private int regenAmount;

        public Meditate(int level, int turnsToRefresh, int healthCost, string name)
        {
            _level = level;
            TurnsToRefresh = turnsToRefresh;
            TurnsUntilRefreshed = 0;
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
            else if (player.Mana == player.MaxMana)
            {
                Game.MessageLog.Add("You decide to not cast the spell as your mana is already at max-out");

                return false;
            }
            else
            {
                player.Health -= _healthCost;

                if (_level == 1)
                {
                    regenAmount = Dice.Roll("3D2") + 1;
                }
                else if (_level == 2)
                {
                    regenAmount = Dice.Roll("3D3") + 3;
                }
                else if (_level == 3)
                {
                    regenAmount = Dice.Roll("4D4") + 4;
                }
                else
                {
                    regenAmount = Dice.Roll("6D5") + 5;
                }

                Game.MessageLog.Add($"{_level}You sacrifice {_healthCost} health points to regenerate {regenAmount} mana points");
                player.Mana = Math.Min(player.MaxMana, player.Mana + regenAmount);

                return true;
            }
        }
    }
}
