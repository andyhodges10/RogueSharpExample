using RogueSharpExample.Core;
using RogueSharpExample.Traps;

namespace RogueSharpExample.Systems
{
    public static class TrapGenerator
    {
        public static Trap CreateTrap(int level)
        {
            Pool<Trap> trapPool = new Pool<Trap>();

            if (level <= 3)
            {
                trapPool.Add(new BearTrap(), 100);
            }
            else if (level <= 6)
            {
                trapPool.Add(new BearTrap(), 100);
            }
            else
            {
                trapPool.Add(new BearTrap(), 100);
            }

            return trapPool.Get();
        }
    }
}
