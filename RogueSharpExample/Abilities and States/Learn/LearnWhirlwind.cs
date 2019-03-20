using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class LearnWhirlwind : Ability
    {
        private readonly int _range;
        private readonly int _manaCost;
        private readonly int _poisonChance;
        private readonly int _poisonLength;
        private readonly int _poisonDamage;

        public LearnWhirlwind(int range, int turnsToRefresh, int manaCost, int poisonChance, int poisonLength, int poisonDamage, string name)
        {
            _range = range;
            TurnsToRefresh = turnsToRefresh;
            TurnsUntilRefreshed = 0;
            _manaCost = manaCost;
            _poisonChance = poisonChance;
            _poisonLength = poisonLength;
            _poisonDamage = poisonDamage;
            Name = name;
        }

        protected override bool PerformAbility()
        {
            Player player = Game.Player;
            player.QAbility = new Whirlwind(_range, TurnsToRefresh, _manaCost, _poisonChance, _poisonLength, _poisonDamage, Name);

            return true;
        }
    }
}
