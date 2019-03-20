using RogueSharpExample.Core;

namespace RogueSharpExample.Abilities
{
   public class DoNothing : Ability
   {
      public DoNothing()
      {
         Name = "None";
         TurnsToRefresh = 0;
         TurnsUntilRefreshed = 0;
      }

      protected override bool PerformAbility()
      {
         Game.MessageLog.Add( "No ability in that slot" ); // debug
         return false;
      }
   }
}
