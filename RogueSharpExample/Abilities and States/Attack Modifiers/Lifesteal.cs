using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
    class Lifesteal : Ability
    {
        private readonly int _lifestealChance;

        public Lifesteal(int lifestealChance, string name)
        {
            _lifestealChance = lifestealChance;
            Name = name;
        }

        protected override bool PerformAbility()
        {
            if (Dice.Roll("1D100") < _lifestealChance)
            {
                int _lifesteal = Dice.Roll("2D4");
                Game.MessageLog.Add($"The {Game.Player.Hand.Name} steals {_lifesteal} health points from the {Victim.Name}", Colors.Healing);
                RegainHP regainHP = new RegainHP(_lifesteal, 0);
                regainHP.Perform();
            }

            return true;
        }
    }
}
