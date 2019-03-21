using System.Collections.Generic;
using System.Linq;
using RLNET;
using RogueSharp;
using RogueSharpExample.Interfaces;

namespace RogueSharpExample.Core
{
    public class DungeonMap : Map
    {
        public List<Rectangle> Rooms { get; set; }
        public List<Door> Doors { get; set; }
        public Stairs StairsUp { get; set; }
        public Stairs StairsDown { get; set; }
        public List<Plant> Plants = new List<Plant>();

        private readonly List<Monster> _monsters;
        private readonly List<TreasurePile> _treasurePiles;
        private readonly List<Item> _items;
        private readonly List<Trap> _traps;
        private readonly List<Actor> _shopkeepers;
        private readonly List<Actor> _explorers;

        public DungeonMap()
        {
            Game.SchedulingSystem.Clear();
            
            Rooms = new List<Rectangle>();
            Doors = new List<Door>();

            _monsters = new List<Monster>();
            _treasurePiles = new List<TreasurePile>();
            _items = new List<Item>();
            _traps = new List<Trap>();
            _shopkeepers = new List<Actor>(); 
            _explorers = new List<Actor>();
        }

        public void AddMonster(Monster monster)
        {
            _monsters.Add(monster);
            SetIsWalkable(monster.X, monster.Y, false);
            Game.SchedulingSystem.Add(monster);
        }

        public void AddShopkeeper(Actor shopkeeper)
        {
            _shopkeepers.Add(shopkeeper);
            SetIsWalkable(shopkeeper.X, shopkeeper.Y, false);
            Game.SchedulingSystem.Add(shopkeeper);
        }

        public void AddExplorer(Actor explorer)
        {
            _explorers.Add(explorer);
            SetIsWalkable(explorer.X, explorer.Y, false);
            Game.SchedulingSystem.Add(explorer);
        }

        public void RemoveMonster(Monster monster)
        {
            Player player = Game.Player;
            player.Experience -= monster.Experience;
            player.TotalExperience += monster.Experience;

            if (player.Experience <= 0 && player.Level < player.MaxLevel)
            {
                player.LevelUp();
            }
            else if (player.Experience <= 0 && player.Level >= player.MaxLevel)
            {
                Game.MessageLog.Add("You are already max level");
                player.Experience = 9999;
            }

            _monsters.Remove(monster);
            SetIsWalkable(monster.X, monster.Y, true);
            Game.SchedulingSystem.Remove(monster);
        }

        public Monster GetMonsterAt(int x, int y)
        {
            // Known Bug: Should be single, but sometimes actors occupy the same space.
            return _monsters.FirstOrDefault(m => m.X == x && m.Y == y);
        }
        public Actor GetShopkeeperAt(int x, int y)
        {
            return _shopkeepers.FirstOrDefault(m => m.X == x && m.Y == y);
        }
        public Actor GetExplorerAt(int x, int y)
        {
            return _explorers.FirstOrDefault(m => m.X == x && m.Y == y);
        }
        
        public bool CheckForPlayer(int x, int y)
        {
            Player player = Game.Player;
            if (x == player.X && y == player.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Plant> GetPlantsAt(int x, int y)
        {
            return Plants.Where(m => m.X == x && m.Y == y).ToList();
        }
        public List<TreasurePile> GetItemsAt(int x, int y)
        {
            return _treasurePiles.Where(m => m.X == x && m.Y == y).ToList();
        }

        public IEnumerable<Point> GetMonsterLocations()
        {
            return _monsters.Select(m => new Point {
                X = m.X,
                Y = m.Y
            });
        }

        public IEnumerable<Point> GetMonsterLocationsInFieldOfView()
        {
            return _monsters.Where(monster => IsInFov(monster.X, monster.Y))
               .Select(m => new Point { X = m.X, Y = m.Y });
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public void AddTreasure(int x, int y, ITreasure treasure)
        {
            _treasurePiles.Add(new TreasurePile(x, y, treasure));
        }

        public void AddPlayer(Player player)
        {
            Game.Player = player;
            SetIsWalkable(player.X, player.Y, false);
            UpdatePlayerFieldOfView();
            Game.SchedulingSystem.Add(player);
        }

        public void UpdatePlayerFieldOfView()
        {
            Player player = Game.Player;
            ComputeFov(player.X, player.Y, player.Awareness, true);

            foreach (ICell cell in GetAllCells())
            {
                if (IsInFov(cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }

        public bool SetActorPosition(Actor actor, int x, int y)
        {
            if (GetCell(x, y).IsWalkable)
            {
                PickUpTreasure(actor, x, y);
                SetIsWalkable(actor.X, actor.Y, true);
                actor.X = x;
                actor.Y = y;
                SetIsWalkable(actor.X, actor.Y, false);
                OpenDoor(actor, x, y);
                if (actor is Player)
                {
                    UpdatePlayerFieldOfView();
                }
                return true;
            }
            return false;
        }

        public Door GetDoor(int x, int y)
        {
            return Doors.SingleOrDefault(d => d.X == x && d.Y == y);
        }

        private void OpenDoor(Actor actor, int x, int y)
        {
            Door door = GetDoor(x, y);
            if (door != null && !door.IsOpen)
            {
                door.IsOpen = true;
                var cell = GetCell(x, y);
                SetCellProperties(x, y, true, cell.IsWalkable, cell.IsExplored);

                Game.MessageLog.Add($"You opened a door");
            }
        }

        public void AddGold(int x, int y, int amount)
        {
            if (amount > 0)
            {
                AddTreasure(x, y, new Gold(amount));
            }
        }

        private void PickUpTreasure(Actor actor, int x, int y)
        {
            List<TreasurePile> treasureAtLocation = _treasurePiles.Where(g => g.X == x && g.Y == y).ToList();
            foreach (TreasurePile treasurePile in treasureAtLocation)
            {
                if (treasurePile.Treasure.PickUp(actor))
                {
                    _treasurePiles.Remove(treasurePile);
                }
            }
        }

        public void RemoveTrap(int x, int y)
        {
            List<TreasurePile> trapAtLocation = _treasurePiles.Where(g => g.X == x && g.Y == y).ToList();
            foreach (TreasurePile trap in trapAtLocation)
            {
                _treasurePiles.Remove(trap);
            }
        }

        public bool CanMoveDownToNextLevel()
        {
            Player player = Game.Player;
            if (StairsDown != null)
            {
                return StairsDown.X == player.X && StairsDown.Y == player.Y;
            }
            return false;
        }

        public bool CanMoveUpToNextLevel()
        {
            Player player = Game.Player;
            if (StairsUp != null)
            {
                return StairsUp.X == player.X && StairsUp.Y == player.Y;
            }
            return false;
        }

        public void SetIsWalkable(int x, int y, bool isWalkable)
        {
            ICell cell = GetCell(x, y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }

        public Point GetRandomLocation()
        {
            int roomNumber = Game.Random.Next(0, Rooms.Count - 1);
            Rectangle randomRoom = Rooms[roomNumber];

            if (!DoesRoomHaveWalkableSpace(randomRoom))
            {
                GetRandomLocation();
            }

            return GetRandomLocationInRoom(randomRoom);
        }

        // actors sometimes get stuck in stairs or in player's spawn
        public Point GetRandomLocationInFirstRoom(Rectangle room)
        {
            int x = Game.Random.Next(1, room.Width - 2) + room.X;
            int y = Game.Random.Next(1, room.Height - 2) + room.Y;

            if (x == room.Center.X + 1 && y == room.Center.Y)
            {
                GetRandomLocationInRoom(room);
            }
            else if (x == room.Center.X && y == room.Center.Y)
            {
                GetRandomLocationInRoom(room);
            }

            if (!IsWalkable(x, y))
            {
                GetRandomLocationInRoom(room);
            }

            return new Point(x, y);
        }

        public Point GetRandomLocationInRoom(Rectangle room)
        {
            int x = Game.Random.Next(1, room.Width - 2) + room.X;
            int y = Game.Random.Next(1, room.Height - 2) + room.Y;

            if (!IsWalkable(x, y))
            {
                GetRandomLocationInRoom(room);
            }
            
            return new Point(x, y);
        }

        public bool DoesRoomHaveWalkableSpace(Rectangle room)
        {
            for (int x = 1; x <= room.Width - 2; x++)
            {
                for (int y = 1; y <= room.Height - 2; y++)
                {
                    if (IsWalkable(x + room.X, y + room.Y))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Draw(RLConsole mapConsole, RLConsole statConsole)
        {
            mapConsole.Clear();
            foreach (ICell cell in GetAllCells())
            {
                SetConsoleSymbolForCell(mapConsole, cell);
            }
            foreach (Door door in Doors)
            {
                door.Draw(mapConsole, this);
            }
            foreach (Plant plant in Plants)
            {
                plant.Draw(mapConsole, this);
            }

            if (StairsUp != null)
            {
                StairsUp.Draw(mapConsole, this);
            }
            if (StairsDown != null)
            {
                StairsDown.Draw(mapConsole, this);
            }

            foreach (TreasurePile treasurePile in _treasurePiles)
            {
                IDrawable drawableTreasure = treasurePile.Treasure as IDrawable;
                drawableTreasure?.Draw(mapConsole, this);
            }
            foreach (Item item in _items)
            {
                IDrawable drawableItem = item as IDrawable;
                drawableItem?.Draw(mapConsole, this);
            }
            foreach (Trap trap in _traps)
            {
                IDrawable drawableTrap = trap as IDrawable;
                drawableTrap?.Draw(mapConsole, this);
            }
            foreach (Actor shopkeeper in _shopkeepers)
            {
                shopkeeper.Draw(mapConsole, this);
            }
            foreach (Actor explorer in _explorers)
            {
                explorer.Draw(mapConsole, this);
            }

            statConsole.Clear();
            int i = 0;
            foreach (Monster monster in _monsters)
            {
                monster.Draw(mapConsole, this);

                if (IsInFov(monster.X, monster.Y) && monster.IsMimicInHiding == false)
                {
                    monster.DrawStats(statConsole, i);
                    i++;
                }
            }
        }

        private void SetConsoleSymbolForCell(RLConsole console, ICell cell)
        {
            if (!cell.IsExplored)
            {
                return;
            }

            double distance = Game.DistanceBetween(Game.Player.X, Game.Player.Y, cell.X, cell.Y);
            float blendRatio = .5f / Game.Player.Awareness;
            float blendAmount = (float)(blendRatio * distance);

            // Floor values used in Actor.cs
            if (Game.MapLevel < 3)
            {
                if (IsInFov(cell.X, cell.Y))
                {

                    if (cell.IsWalkable)
                    {
                        console.Set(cell.X, cell.Y, RLColor.Blend(Colors.LowLevelFloorFov, Colors.LowLevelFloor, .5f - blendAmount), RLColor.Blend(Colors.BackgroundFov1, Colors.BackgroundFov2, .5f - blendAmount), '.');
                    }
                    else
                    {
                        console.Set(cell.X, cell.Y, RLColor.Blend(Colors.LowLevelWallFov, Colors.LowLevelWall, .5f - blendAmount), RLColor.Blend(Colors.BackgroundFov1, Colors.BackgroundFov2, .5f - blendAmount), '#');
                    }
                }
                else
                {
                    if (cell.IsWalkable)
                    {
                        console.Set(cell.X, cell.Y, Colors.LowLevelFloor, Colors.Background, '.');
                    }
                    else
                    {
                        console.Set(cell.X, cell.Y, Colors.LowLevelWall, Colors.Background, '#');
                    }
                }
            }
            else if (Game.MapLevel < 5)
            {
                if (IsInFov(cell.X, cell.Y))
                {

                    if (cell.IsWalkable)
                    {
                        console.Set(cell.X, cell.Y, RLColor.Blend(Colors.FloorFov, Colors.Floor, .5f - blendAmount), RLColor.Blend(Colors.BackgroundFov1, Colors.BackgroundFov2, .5f - blendAmount), '.');
                    }
                    else
                    {
                        console.Set(cell.X, cell.Y, RLColor.Blend(Colors.WallFov, Colors.Wall, .5f - blendAmount), RLColor.Blend(Colors.BackgroundFov1, Colors.BackgroundFov2, .5f - blendAmount), '#');
                    }
                }
                else
                {
                    if (cell.IsWalkable)
                    {
                        console.Set(cell.X, cell.Y, Colors.Floor, Colors.Background, '.');
                    }
                    else
                    {
                        console.Set(cell.X, cell.Y, Colors.Wall, Colors.Background, '#');
                    }
                }
            }
            else if (Game.MapLevel < 7)
            {
                if (IsInFov(cell.X, cell.Y))
                {

                    if (cell.IsWalkable)
                    {
                        console.Set(cell.X, cell.Y, RLColor.Blend(Colors.IceFloorFov, Colors.IceFloor, .5f - blendAmount), RLColor.Blend(Colors.BackgroundFov1, Colors.BackgroundFov2, .5f - blendAmount), '.');
                    }
                    else
                    {
                        console.Set(cell.X, cell.Y, RLColor.Blend(Colors.IceWallFov, Colors.IceWall, .5f - blendAmount), RLColor.Blend(Colors.BackgroundFov1, Colors.BackgroundFov2, .5f - blendAmount), '#');
                    }
                }
                else
                {
                    if (cell.IsWalkable)
                    {
                        console.Set(cell.X, cell.Y, Colors.IceFloor, Colors.Background, '.');
                    }
                    else
                    {
                        console.Set(cell.X, cell.Y, Colors.IceWall, Colors.Background, '#');
                    }
                }
            }
            else if (Game.MapLevel < 9)
            {
                if (IsInFov(cell.X, cell.Y))
                {

                    if (cell.IsWalkable)
                    {
                        console.Set(cell.X, cell.Y, RLColor.Blend(Colors.CaveFloorFov, Colors.CaveFloor, .5f - blendAmount), RLColor.Blend(Colors.BackgroundFov1, Colors.BackgroundFov2, .5f - blendAmount), '.');
                    }
                    else
                    {
                        console.Set(cell.X, cell.Y, RLColor.Blend(Colors.CaveWallFov, Colors.CaveWall, .5f - blendAmount), RLColor.Blend(Colors.BackgroundFov1, Colors.BackgroundFov2, .5f - blendAmount), '#');
                    }
                }

                else
                {
                    if (cell.IsWalkable)
                    {
                        console.Set(cell.X, cell.Y, Colors.CaveFloor, Colors.Background, '.');
                    }
                    else
                    {
                        console.Set(cell.X, cell.Y, Colors.CaveWall, Colors.Background, '#');
                    }
                }
            }
            else
            {
                if (IsInFov(cell.X, cell.Y))
                {
                    if (cell.IsWalkable)
                    {
                        console.Set(cell.X, cell.Y, RLColor.Blend(Colors.HellFloorFov, Colors.HellFloor, .5f - blendAmount), RLColor.Blend(Colors.BackgroundFov1, Colors.BackgroundFov2, .5f - blendAmount), '.');
                    }
                    else
                    {
                        console.Set(cell.X, cell.Y, RLColor.Blend(Colors.HellWallFov, Colors.HellWall, .5f - blendAmount), RLColor.Blend(Colors.BackgroundFov1, Colors.BackgroundFov2, .5f - blendAmount), '#');
                    }
                }
                else
                {
                    if (cell.IsWalkable)
                    {
                        console.Set(cell.X, cell.Y, Colors.HellFloor, Colors.Background, '.');
                    }
                    else
                    {
                        console.Set(cell.X, cell.Y, Colors.HellWall, Colors.Background, '#');
                    }
                }
            }
        }

        public void PlacePlayerNearExit()
        {
            Player player = Game.Player;
            player.X = StairsDown.X;
            player.Y = StairsDown.Y;
            UpdatePlayerFieldOfView();
        }

        public void PlacePlayerNearEntrance()
        {
            Player player = Game.Player;
            player.X = Rooms[0].Center.X;
            player.Y = Rooms[0].Center.Y;
            UpdatePlayerFieldOfView();
        }

        public void RescheduleExistingActors()
        {
            Game.SchedulingSystem.Clear();
            Game.SchedulingSystem.Add(Game.Player);
            foreach (Monster monster in _monsters)
            {
                Game.SchedulingSystem.Add(monster);
            }
            if (StairsUp != null)
            {
                SetIsWalkable(StairsUp.X, StairsUp.Y, true);
            }
            SetIsWalkable(StairsDown.X, StairsDown.Y, true);
        }
    }
}
