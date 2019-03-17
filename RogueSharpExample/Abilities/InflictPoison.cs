using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class InflictPoison : Ability
    {
        private readonly int _poisonChance;
        private readonly int _poisonLength;
        private readonly int _poisonDamage;
        private readonly int _manaCost;

        public InflictPoison(int poisonChance, int poisonLength, int poisonDamage, int manaCost, string name)
        {
            _poisonChance = poisonChance;
            _poisonLength = poisonLength;
            _poisonDamage = poisonDamage;
            _manaCost = manaCost;
            Name = name;
        }

        protected override bool PerformAbility()
        {
            if (Dice.Roll("1D100") < _poisonChance)
            {
                if (Victim.IsPoisonedImmune == false && _poisonDamage != 0)
                {
                    Victim.State = new MonsterAbnormalState(Victim, 3, "Poisoned", -1, -1, _poisonLength, _poisonDamage);
                }
            }

            return true;
        }
    }
}
