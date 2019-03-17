using System;
using System.Linq;
using System.Collections.Generic;
using RogueSharp;
using RogueSharp.DiceNotation;
using RogueSharpExample.Core;

namespace RogueSharpExample.Systems
{
    class MapGenerator
    {
        private readonly int _width;
        private readonly int _height;
        private readonly int _maxRooms;
        private readonly int _roomMaxSize;
        private readonly int _roomMinSize;
        private readonly int _level;
        private readonly DungeonMap _map;
        private readonly EquipmentGenerator _equipmentGenerator;

        public MapGenerator(int width, int height, int maxRooms, int roomMaxSize, int roomMinSize, int mapLevel)
        {
            _width = width;
            _height = height;
            _maxRooms = maxRooms;
            _roomMaxSize = roomMaxSize;
            _roomMinSize = roomMinSize;
            _level = mapLevel;
            _map = new DungeonMap();
            _equipmentGenerator = new EquipmentGenerator(mapLevel);
        }

        public DungeonMap CreateForrest()
        {
            _map.Initialize(_width, _height);

            Rectangle newRoom = new Rectangle(0,0,_width -1,_height -1);
            _map.Rooms.Add(newRoom);

            foreach (Rectangle room in _map.Rooms) {
                CreateMap(room);
            }

            foreach (ICell cell in _map.GetAllCells())
            {
                
                if (cell.X != 0 && cell.Y != 0 && cell.X != _width - 1 && cell.Y != _height - 1)
                {
                    if (Dice.Roll("1d100") > 95)
                    {
                        _map.SetCellProperties(cell.X, cell.Y, false, false, false);
                        _map.Plants.Add(new Tree(cell.X, cell.Y));
                    }
                    else
                    {
                        _map.SetCellProperties(cell.X, cell.Y, true, true, false);
                        _map.Plants.Add(new Grass(cell.X, cell.Y));
                    }
                }
            }

            PlacePlayerInForrest();
            CreateForrestStairs();
            PlaceMonstersInForrest();
            PlaceItemsInForrest();
            PlaceEquipmentInForrest();

            return _map;
        }

        public DungeonMap CreateMap()
        {
            _map.Initialize(_width, _height);

            for (int r = _maxRooms; r > 0; r--)
            {
                int roomWidth = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomHeight = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomXPosition = Game.Random.Next(0, _width - roomWidth - 1);
                int roomYPosition = Game.Random.Next(0, _height - roomHeight - 1);

                Rectangle newRoom = new Rectangle(roomXPosition, roomYPosition, roomWidth, roomHeight);

                bool newRoomIntersects = _map.Rooms.Any(room => newRoom.Intersects(room));

                if (!newRoomIntersects)
                {
                    _map.Rooms.Add(newRoom);
                }
            }

            foreach (Rectangle room in _map.Rooms)
            {
                CreateMap(room);
            }

            for (int r = 0; r < _map.Rooms.Count; r++)
            {
                if (r == 0)
                {
                    continue;
                }

                int previousRoomCenterX = _map.Rooms[r - 1].Center.X;
                int previousRoomCenterY = _map.Rooms[r - 1].Center.Y;
                int currentRoomCenterX = _map.Rooms[r].Center.X;
                int currentRoomCenterY = _map.Rooms[r].Center.Y;

                if (Game.Random.Next(0, 2) == 0)
                {
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, currentRoomCenterX);
                }
                else
                {
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY);
                }
            }

            foreach (Rectangle room in _map.Rooms)
            {
                CreateDoors(room);
            }

            if ( _level == 5 || _level == 7 || _level == 9)
            {
                PlaceMimics();
            }

            if (_level == 4 || _level == 6 || _level == 8 || _level == 10)
            {
                PlaceBoss();
            }

            if (_level == 4 || _level == 6 || _level == 8 || _level == 10)
            {
                PlaceShopkeeper();
            }

            if (_level == 2 || _level == 5 || _level == 7)
            {
                PlaceExplorer();
            }

            CreateStairs();
            PlacePlayer();
            PlaceMonsters();
            PlaceEquipment();
            PlaceItems();

            return _map;
        }

        private void CreateMap(Rectangle room)
        {
            for (int x = room.Left + 1; x < room.Right; x++)
            {
                for (int y = room.Top + 1; y < room.Bottom; y++)
                {
                    _map.SetCellProperties(x, y, true, true);
                }
            }
        }

        private void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition)
        {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
            {
                _map.SetCellProperties(x, yPosition, true, true);
            }
        }

        private void CreateVerticalTunnel(int yStart, int yEnd, int xPosition)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
            {
                _map.SetCellProperties(xPosition, y, true, true);
            }
        }

        private void CreateDoors(Rectangle room)
        {
            int xMin = room.Left;
            int xMax = room.Right;
            int yMin = room.Top;
            int yMax = room.Bottom;

            List<ICell> borderCells = _map.GetCellsAlongLine(xMin, yMin, xMax, yMin).ToList();
            borderCells.AddRange(_map.GetCellsAlongLine(xMin, yMin, xMin, yMax));
            borderCells.AddRange(_map.GetCellsAlongLine(xMin, yMax, xMax, yMax));
            borderCells.AddRange(_map.GetCellsAlongLine(xMax, yMin, xMax, yMax));

            foreach (ICell cell in borderCells)
            {
                if (IsPotentialDoor(cell))
                {
                    _map.SetCellProperties(cell.X, cell.Y, false, true);
                    _map.Doors.Add(new Door
                    {
                        X = cell.X,
                        Y = cell.Y,
                        IsOpen = false
                    });
                }
            }
        }

        private bool IsPotentialDoor(ICell cell)
        {
            if (!cell.IsWalkable)
            {
                return false;
            }

            ICell right = _map.GetCell(cell.X + 1, cell.Y);
            ICell left = _map.GetCell(cell.X - 1, cell.Y);
            ICell top = _map.GetCell(cell.X, cell.Y - 1);
            ICell bottom = _map.GetCell(cell.X, cell.Y + 1);

            if (_map.GetDoor(cell.X, cell.Y) != null || _map.GetDoor(right.X, right.Y) != null ||
                 _map.GetDoor(left.X, left.Y) != null || _map.GetDoor(top.X, top.Y) != null ||
                 _map.GetDoor(bottom.X, bottom.Y) != null)
            {
                return false;
            }

            if (right.IsWalkable && left.IsWalkable && !top.IsWalkable && !bottom.IsWalkable)
            {
                return true;
            }

            if (!right.IsWalkable && !left.IsWalkable && top.IsWalkable && bottom.IsWalkable)
            {
                return true;
            }
            return false;
        }

        private void CreateStairs()
        {
            _map.StairsUp = new Stairs {
                X = _map.Rooms.First().Center.X + 1,
                Y = _map.Rooms.First().Center.Y,
                IsUp = true
            };
            _map.StairsDown = new Stairs {
                X = _map.Rooms.Last().Center.X,
                Y = _map.Rooms.Last().Center.Y,
                IsUp = false
            };
        }

        private void CreateForrestStairs()
        {
            if (_map.GetCell(_map.Rooms.First().Center.X, _map.Rooms.First().Center.Y).IsWalkable)
            {
                _map.StairsDown = new Stairs
                {
                    X = _map.Rooms.First().Center.X,
                    Y = _map.Rooms.First().Center.Y,
                    IsUp = false
                };
            }
            else
            {
                string mapDiceWidthExpression = "1d" + (_map.Width - 2);
                string mapDiceHeightExpression = "1d" + (_map.Height - 2);
                int X = Dice.Roll(mapDiceWidthExpression);
                int Y = Dice.Roll(mapDiceHeightExpression);

                // not working as fully intended
                while (!_map.GetCell(X, Y).IsWalkable)
                {
                    X = Dice.Roll(mapDiceWidthExpression);
                    Y = Dice.Roll(mapDiceHeightExpression);
                }

                _map.StairsDown = new Stairs
                {
                    X = Dice.Roll(mapDiceWidthExpression),
                    Y = Dice.Roll(mapDiceHeightExpression),
                    IsUp = false
                };
            }
        }

        private void PlaceMonsters()
        {
            int roomNumber = 0;
            foreach (var room in _map.Rooms)
            {
                if (Dice.Roll("1D10") < 6 + (_level / 3))
                {
                    var numberOfMonsters = Dice.Roll("1D2") + (_level / 2);
                    for (int i = 0; i < numberOfMonsters; i++)
                    {
                        if (_map.DoesRoomHaveWalkableSpace(room) && roomNumber != 0)
                        {
                            Point randomRoomLocation = _map.GetRandomLocationInRoom(room);
                            if (randomRoomLocation != null)
                            {
                                _map.AddMonster(ActorGenerator.CreateMonster(_level, _map.GetRandomLocationInRoom(room)));
                            }
                        }
                    }
                }
                roomNumber++;
            }
        }

        private void PlaceMonstersInForrest()
        {
            foreach (var room in _map.Rooms)
            {
                var numberOfMonsters = Dice.Roll("3D2") + 5;
                for (int i = 0; i < numberOfMonsters; i++)
                {
                    if (_map.DoesRoomHaveWalkableSpace(room))
                    {
                        Point randomRoomLocation = _map.GetRandomLocationInRoom(room);
                        if (randomRoomLocation != null)
                        {
                            _map.AddMonster(ActorGenerator.CreateMonster(_level, _map.GetRandomLocationInRoom(room)));
                        }
                    }
                }
            }
        }

        private void PlaceItemsInForrest()
        {
            foreach (var room in _map.Rooms)
            {
                var numberOfItems = Dice.Roll("1D2") + 1;
                for (int i = 0; i < numberOfItems; i++)
                {
                    if (_map.DoesRoomHaveWalkableSpace(room))
                    {
                        Point randomRoomLocation = _map.GetRandomLocationInRoom(room);
                        if (randomRoomLocation != null)
                        {
                            Item item = ItemGenerator.CreateItem(_level);

                            Point location = _map.GetRandomLocationInRoom(room);
                            _map.AddTreasure(location.X, location.Y, item);
                        }
                    }
                }
            }
        }

        private void PlaceEquipmentInForrest()
        {
            foreach (var room in _map.Rooms)
            {
                var numberOfItems = Dice.Roll("1D2");
                for (int i = 0; i < numberOfItems; i++)
                {
                    if (_map.DoesRoomHaveWalkableSpace(room))
                    {
                        Point randomRoomLocation = _map.GetRandomLocationInRoom(room);
                        if (randomRoomLocation != null)
                        {
                            Core.Equipment equipment;
                            try
                            {
                                equipment = _equipmentGenerator.CreateEquipment();
                            }
                            catch (InvalidOperationException)
                            {
                                return;
                            }

                            Point location = _map.GetRandomLocationInRoom(room);
                            _map.AddTreasure(location.X, location.Y, equipment);
                        }
                    }
                }
            }
        }

        private void PlaceEquipment()
        {
            foreach (var room in _map.Rooms)
            {
                if (Dice.Roll("1D10") > 7)
                {
                    if (_map.DoesRoomHaveWalkableSpace(room))
                    {
                        Point randomRoomLocation = _map.GetRandomLocationInRoom(room);
                        if (randomRoomLocation != null)
                        {
                            Core.Equipment equipment;
                            try
                            {
                                equipment = _equipmentGenerator.CreateEquipment();
                            }
                            catch (InvalidOperationException)
                            {
                                return;
                            }

                            Point location = _map.GetRandomLocationInRoom(room);
                            _map.AddTreasure(location.X, location.Y, equipment);
                        }
                    }
                }
            }
        }

        private void PlaceItems()
        {
            foreach (var room in _map.Rooms)
            {
                if (Dice.Roll("1D10") > 7)
                {
                    if (_map.DoesRoomHaveWalkableSpace(room))
                    {
                        Point randomRoomLocation = _map.GetRandomLocationInRoom(room);
                        if (randomRoomLocation != null)
                        {
                            Item item = ItemGenerator.CreateItem(_level);

                            Point location = _map.GetRandomLocationInRoom(room);
                            _map.AddTreasure(location.X, location.Y, item);
                        }
                    }
                }
            }
        }

        private void PlacePlayer()
        {
            Player player = Game.Player;
            if (player == null)
            {
                player = new Player();
            }

            player.X = _map.Rooms[0].Center.X;
            player.Y = _map.Rooms[0].Center.Y;
            
            _map.AddPlayer(player);
        }

        private void PlacePlayerInForrest()
        {
            Player player = Game.Player;
            if (player == null)
            {
                player = new Player();
            }

            int middleX = (int)_map.Width / 2;
            int middleY = (int)_map.Height / 2;

            while (!_map.GetCell(middleX, middleY).IsWalkable)
            {
                if (Dice.Roll("1d2") > 1)
                {
                    ++middleX;
                }
                else
                {
                    ++middleY;
                }
                if (Dice.Roll("1d2") > 1)
                {
                    --middleX;
                }
                else
                {
                    --middleY;
                }
            }

            player.X = middleX;
            player.Y = middleY;

            _map.AddPlayer(player);
        }

        private void PlaceShopkeeper()
        {
            Rectangle room = _map.Rooms.First();
            _map.AddShopkeeper(ActorGenerator.CreateNPC(_level, _map.GetRandomLocationInRoom(room)));
        }

        private void PlaceExplorer()
        {
            Rectangle room = _map.Rooms.First();
            _map.AddExplorer(ActorGenerator.CreateNPC(_level, _map.GetRandomLocationInRoom(room)));
        }

        private void PlaceBoss()
        {
            _map.AddMonster(ActorGenerator.CreateBoss(_level, _map.Rooms.Last().Center));
        }

        private void PlaceMimics()
        {
            Rectangle room = _map.Rooms.Last(); // hp improve me

            if (_map.DoesRoomHaveWalkableSpace(room))
            {
                Point randomRoomLocation = _map.GetRandomLocationInRoom(room);
                if (randomRoomLocation != null)
                {
                    _map.AddMonster(ActorGenerator.CreateMimic(_level, _map.GetRandomLocationInRoom(room)));
                }
            }
        }
    }
}

