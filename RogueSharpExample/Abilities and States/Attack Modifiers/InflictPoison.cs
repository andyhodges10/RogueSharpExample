﻿using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    public class InflictPoison : Ability
    {
        private readonly int _poisonChance;
        private readonly int _poisonLength;
        private readonly int _poisonDamage;

        public InflictPoison(int poisonChance, int poisonLength, int poisonDamage, string name)
        {
            _poisonChance = poisonChance;
            _poisonLength = poisonLength;
            _poisonDamage = poisonDamage;
            Name = name;
        }

        protected override bool PerformAbility()
        {
            if (Dice.Roll("1D100") < _poisonChance)
            {
                if (Victim.IsPoisonedImmune == false && _poisonDamage != 0)
                {
                    Victim.State = new MonsterAbnormalState(Victim, _poisonLength, "Poisoned", -1, -1, _poisonDamage);
                }
            }

            return true;
        }
    }
}
