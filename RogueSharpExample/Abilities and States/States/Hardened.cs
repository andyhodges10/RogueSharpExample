using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class Hardened : Ability
    {
        private readonly int _manaCost;

        public Hardened(int turnsToRefresh, int manaCost, string name)
        {
            TurnsToRefresh = turnsToRefresh;
            TurnsUntilRefreshed = 0;
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
                player.State = new AbnormalState(TurnsToRefresh, "Hardened", "", 1, 1, 2, -2, 0);

                return true;
            }
        }
    }
}
