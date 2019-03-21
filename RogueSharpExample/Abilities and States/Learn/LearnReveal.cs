using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class LearnReveal : Ability // do me
    {
        private readonly int _manaCost;

        public LearnReveal(int turnsToRefresh, int manaCost, string name)
        {
            TurnsToRefresh = turnsToRefresh;
            _manaCost = manaCost;
            Name = name;
        }

        protected override bool PerformAbility()
        {
            Player player = Game.Player;
            player.QAbility = new Reveal(TurnsToRefresh, _manaCost, Name);

            return true;
        }
    }
}
