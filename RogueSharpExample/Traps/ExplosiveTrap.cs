using RLNET;
using RogueSharp.DiceNotation;
using RogueSharpExample.Core;
using RogueSharpExample.Abilities;

namespace RogueSharpExample.Traps
{
    public class ExplosiveTrap : Trap
    {
        public ExplosiveTrap()
        {
            Name = "PoisonTrap";
            Color = Colors.KoboldColor;
            Symbol = '^';
        }

        protected override bool TrapTriggered()
        {
            int _damage = Dice.Roll("5D4");
            Game.MessageLog.Add($"You stepped on a {Name} and take {_damage} damage", Swatch.DbBlood);

            Game.Player.Health -= _damage;
            if (Game.Player.Health <= 0)
            {
                Game.Player.Status = "Dead";
            }

            return true;
        }
    }
}
