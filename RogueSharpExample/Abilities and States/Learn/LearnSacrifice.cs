using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class LearnSacrifice : Ability
    {
        private readonly int _level;
        private readonly int _healthCost;

        public LearnSacrifice(int level, int turnsToRefresh, int healthCost, string name)
        {
            TurnsToRefresh = turnsToRefresh;
            _level = level;
            _healthCost = healthCost;
            Name = name;
        }

        protected override bool PerformAbility()
        {
            Player player = Game.Player;
            player.QAbility = new Meditate(_level, TurnsToRefresh, _healthCost, Name);

            return true;
        }
    }
}
