using RLNET;
using RogueSharp;
using RogueSharpExample.Interfaces;

namespace RogueSharpExample.Core
{
    public class Ability : IAbility, ITreasure, IDrawable
    {
        public Ability()
        {
            Symbol = '*';
            Color = RLColor.Yellow;
        }

        public int TurnsToRefresh { get; protected set; }
        public int TurnsUntilRefreshed { get; protected set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Monster Victim { get; set; }

        public bool Perform()
        {
            if (TurnsUntilRefreshed > 0)
            {
                return false;
            }

            TurnsUntilRefreshed = TurnsToRefresh;

            return PerformAbility();
        }

        protected virtual bool PerformAbility()
        {
            return false;
        }


        public void Tick()
        {
            if (TurnsUntilRefreshed > 0)
            {
                TurnsUntilRefreshed--;
            }
        }

        public bool PickUp(IActor actor) // Currently unused
        {
            if (actor is Player player)
            {
                if (player.AddAbility(this))
                {
                    Game.MessageLog.Add($"{actor.Name} learned the {Name} ability");
                    return true;
                }
            }

            return false;
        }

        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public void Draw(RLConsole console, IMap map)
        {
            if (!map.IsExplored(X, Y))
            {
                return;
            }

            if (map.IsInFov(X, Y))
            {
                console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            }
            else
            {
                console.Set(X, Y, RLColor.Blend(Color, RLColor.Gray, 0.5f), Colors.Background, Symbol);
            }
        }
    }
}
