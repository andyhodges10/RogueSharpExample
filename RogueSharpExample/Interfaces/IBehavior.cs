using RogueSharpExample.Core;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Interfaces
{
    public interface IBehavior
    {
        bool Act(Monster monster, CommandSystem commandSystem);
    }
}
