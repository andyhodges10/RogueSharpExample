using RLNET;
using RogueSharpExample.Systems;

namespace RogueSharpExample.Interfaces
{
    public interface IInputSystem
    {
        bool GetInput(RLRootConsole rootConsole, CommandSystem commandSystem);
    }
}
