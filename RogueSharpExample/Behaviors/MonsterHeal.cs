using RogueSharpExample.Core;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Behaviors
{
    public class MonsterHeal : IBehavior
    {
        public bool Act(Monster monster, CommandSystem commandSystem)
        {
            if (monster.Health < monster.MaxHealth)
            {
                int healthToRecover = (int)(monster.MaxHealth/1.25f) - monster.Health;
                monster.Health = monster.Health += healthToRecover;
                Game.MessageLog.Add($"{monster.Name} catches his breath and recovers {healthToRecover} health");
                return true;
            }
            return false;
        }
    }
}
