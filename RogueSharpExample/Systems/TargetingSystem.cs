using System.Collections.Generic;
using System.Linq;
using RLNET;
using RogueSharp;
using RogueSharpExample.Core;
using RogueSharpExample.Interfaces;

namespace RogueSharpExample.Systems
{
    public class TargetingSystem
    {
        private enum SelectionType
        {
            None = 0,
            Target = 1,
            Area = 2,
            Line = 3
        }

        public bool IsPlayerTargeting { get; private set; }

        private Point _cursorPosition;
        private List<Point> _selectableTargets = new List<Point>();
        private int _currentTargetIndex;
        private ITargetable _targetable;
        private int _area;
        private SelectionType _selectionType;

        public bool SelectMonster(ITargetable targetable)
        {
            Initialize();
            _selectionType = SelectionType.Target;
            DungeonMap map = Game.DungeonMap;
            _selectableTargets = map.GetMonsterLocationsInFieldOfView().ToList();
            _targetable = targetable;
            _cursorPosition = _selectableTargets.FirstOrDefault();
            if (_cursorPosition == Point.Zero)
            {
                Game.MessageLog.Add($"You curse at yourself as there are no targetable monster", Swatch.DbBlood);
                StopTargeting();
                return false;
            }

            IsPlayerTargeting = true;
            return true;
        }

        public bool SelectArea(ITargetable targetable, int area = 0)
        {
            Initialize();
            _selectionType = SelectionType.Area;
            Player player = Game.Player;
            _cursorPosition = new Point { X = player.X, Y = player.Y };
            _targetable = targetable;
            _area = area;

            IsPlayerTargeting = true;
            return true;
        }

        public bool SelectLine(ITargetable targetable)
        {
            Initialize();
            _selectionType = SelectionType.Line;
            Player player = Game.Player;
            _cursorPosition = new Point { X = player.X, Y = player.Y };
            _targetable = targetable;

            IsPlayerTargeting = true;
            return true;
        }

        private void StopTargeting()
        {
            IsPlayerTargeting = false;
            Initialize();
        }

        private void Initialize()
        {
            _cursorPosition = Point.Zero;
            _selectableTargets = new List<Point>();
            _currentTargetIndex = 0;
            _area = 0;
            _targetable = null;
            _selectionType = SelectionType.None;
        }

        public bool HandleKey(RLKey key)
        {
            if (_selectionType == SelectionType.Target)
            {
                HandleSelectableTargeting(key);
            }
            else if (_selectionType == SelectionType.Area)
            {
                HandleLocationTargeting(key);
            }
            else if (_selectionType == SelectionType.Line)
            {
                HandleLocationTargeting(key);
            }

            if (key == RLKey.Enter || key == RLKey.KeypadEnter || key == RLKey.Keypad5 || key == RLKey.KeypadPeriod)
            {
                _targetable.SelectTarget(_cursorPosition);
                StopTargeting();
                return true;
            }

            return false;
        }

        private void HandleSelectableTargeting(RLKey key)
        {
            
            if (key == RLKey.Right || key == RLKey.Down || key == RLKey.Keypad6 || key == RLKey.Keypad2)
            {
                
                _currentTargetIndex++;
                if (_currentTargetIndex >= _selectableTargets.Count)
                {
                    _currentTargetIndex = 0;
                }
                _cursorPosition = _selectableTargets[_currentTargetIndex];
            }
            else if (key == RLKey.Left || key == RLKey.Up || key == RLKey.Keypad4 || key == RLKey.Keypad8)
            {
                _currentTargetIndex--;
                if (_currentTargetIndex < 0)
                {
                    _currentTargetIndex = _selectableTargets.Count - 1;
                }
                _cursorPosition = _selectableTargets[_currentTargetIndex];
            }
        }

        private void HandleLocationTargeting(RLKey key)
        {
            int x = _cursorPosition.X;
            int y = _cursorPosition.Y;
            DungeonMap map = Game.DungeonMap;

            if (key == RLKey.Up || key == RLKey.Keypad8)
            {
                y--;
            }
            else if (key == RLKey.Down || key == RLKey.Keypad2)
            {
                y++;
            }
            else if (key == RLKey.Left || key == RLKey.Keypad4)
            {
                x--;
            }
            else if (key == RLKey.Right || key == RLKey.Keypad6)
            {
                x++;
            }
            else if (key == RLKey.Keypad7)
            {
                y--;
                x--;
            }
            else if (key == RLKey.Keypad9)
            {
                y--;
                x++;
            }
            else if (key == RLKey.Keypad1)
            {
                y++;
                x--;
            }
            else if (key == RLKey.Keypad3)
            {
                y++;
                x++;
            }

            if (map.IsInFov(x, y))
            {
                _cursorPosition.X = x;
                _cursorPosition.Y = y;
            }
        }

        public void Draw(RLConsole mapConsole)
        {
            if (IsPlayerTargeting)
            {
                DungeonMap map = Game.DungeonMap;
                Player player = Game.Player;
                if (_selectionType == SelectionType.Area)
                {
                    foreach (ICell cell in map.GetCellsInCircle(_cursorPosition.X, _cursorPosition.Y, _area))
                    {
                        mapConsole.SetBackColor(cell.X, cell.Y, Swatch.DbSun);
                    }
                }
                else if (_selectionType == SelectionType.Line)
                {
                    foreach (ICell cell in map.GetCellsAlongLine(player.X, player.Y, _cursorPosition.X, _cursorPosition.Y))
                    {
                        mapConsole.SetBackColor(cell.X, cell.Y, Swatch.DbSun);
                    }
                }

                mapConsole.SetBackColor(_cursorPosition.X, _cursorPosition.Y, Swatch.DbLight);
            }
        }
    }
}
