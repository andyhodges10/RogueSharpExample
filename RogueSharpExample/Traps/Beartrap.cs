using RogueSharp.DiceNotation;
using RogueSharpExample.Core;
using RogueSharpExample.Abilities;

namespace RogueSharpExample.Traps
{
    public class BearTrap : Trap
    {
        public BearTrap()
        {
            Name = "BearTrap";
            Color = Colors.Trap;
            Symbol = '^';
        }

        protected override bool TrapTriggered()
        {
            int _damage = Dice.Roll("2D3");
            Game.MessageLog.Add($"You stepped on a {Name} and take {_damage} damage", Swatch.DbBlood);

            Game.Player.Health -= _damage;
            if(Game.Player.Health <= 0)
            {
                Game.Player.Status = "Dead";
            }
            else
            {
                Game.Player.State = new AbnormalState(3, "Stuck", "Your leg is stuck in the beartrap!", -2, -2, -3, 2, 0);
            }

            return true;
        }
    }
}
