using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class LearnHeal : Ability
    {
        private readonly int _level;
        private readonly int _manaCost;

        public LearnHeal(int level, int turnsToRefresh, int manaCost, string name)
        {
            TurnsToRefresh = turnsToRefresh;
            _level = level;
            _manaCost = manaCost;
            Name = name;
        }

        protected override bool PerformAbility()
        {
            Player player = Game.Player;
            player.QAbility = new Heal(_level, TurnsToRefresh, _manaCost, Name);

            return true;
        }
    }
}
