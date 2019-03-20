using System;
using System.Threading;
using System.Collections.Generic;
using RLNET;
using RogueSharp.Random;
using RogueSharpExample.Core;
using RogueSharpExample.Systems;
using RogueSharpExample.Interfaces;
using RogueSharpExample.Screens;

namespace RogueSharpExample
{
    public class Game
    {
        public static bool IsGameOver { get; set; }
        public static bool IsInventoryScreenShowing { get; set; }
        public static bool IsDialogScreenShowing { get; set; }
        public static bool IsShopSelectionScreenShowing { get; set; }
        public static bool IsBuyScreenShowing { get; set; }
        public static bool IsSellScreenShowing { get; set; }

        private static readonly int _screenWidth = 100;
        private static readonly int _screenHeight = 70;
        private static readonly int _mapWidth = 80;
        private static readonly int _mapHeight = 48;
        private static readonly int _messageWidth = 100;
        private static readonly int _messageHeight = 11;
        private static readonly int _statWidth = 20;
        private static readonly int _statHeight = 70;
        private static readonly int _inventoryWidth = 80;
        private static readonly int _inventoryHeight = 11;
        private static readonly int _popupWidth = 58; // currently shared with all popup screens
        private static readonly int _popupHeight = 40;
        private static readonly int _dialogHeight = 8;
        private static readonly int _shopSelectionHeight = 16;

        private static RLRootConsole _rootConsole;
        private static RLConsole _mapConsole;
        private static RLConsole _messageConsole;
        private static RLConsole _statConsole;
        private static RLConsole _inventoryConsole;
        private static bool _renderRequired = true;

        public static int MapLevel = 1;
        public static Player Player { get; set; }
        public static MessageLog MessageLog { get; private set; }
        public static CommandSystem CommandSystem { get; private set; }
        public static SchedulingSystem SchedulingSystem { get; private set; }
        public static TargetingSystem TargetingSystem { get; private set; }
        public static IRandom Random { get; private set; }
        private static List<DungeonMap> DungeonMaps = new List<DungeonMap>();
        public static DungeonMap DungeonMap { get; private set; }
        public static IInputSystem InputSystem { get; private set; }
        private static InventoryScreen InventoryScreen;
        public static DialogScreen DialogScreen;
        public static ShopSelectionScreen ShopSelectionScreen;
        private static BuyScreen BuyScreen; // implement me
        private static SellScreen SellScreen;
        private static int _steps = 0;

        // Menuscreens effect variables
        private static RLColor IntroColor;
        private static int i = 0;
        private static bool goingForward = true;

        public static void Main()
        {
            string fontFileName = "terminal8x8.png";

            int seed = (int)DateTime.UtcNow.Ticks;
            string consoleTitle = $"RoguelikeGame - Level {MapLevel}";
            Random = new DotNetRandom(seed);

            MessageLog = new MessageLog();
            
            
            SchedulingSystem = new SchedulingSystem();
            MapGenerator mapGenerator = new MapGenerator(_mapWidth, _mapHeight, 20, 13, 7, MapLevel);
            DungeonMap = mapGenerator.CreateForrest();
            DungeonMaps.Add(DungeonMap);

            _rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight, 8, 8, 1f, consoleTitle);
            _mapConsole = new RLConsole(_mapWidth, _mapHeight);
            _messageConsole = new RLConsole(_messageWidth, _messageHeight);
            _statConsole = new RLConsole(_statWidth, _statHeight);
            _inventoryConsole = new RLConsole(_inventoryWidth, _inventoryHeight);

            CommandSystem = new CommandSystem();
            TargetingSystem = new TargetingSystem();
            InputSystem = new InputSystemPlaying(); 
            InventoryScreen = new InventoryScreen(_popupWidth, _popupHeight);
            DialogScreen = new DialogScreen(_popupWidth, _dialogHeight);
            ShopSelectionScreen = new ShopSelectionScreen(_popupWidth, _shopSelectionHeight);
            BuyScreen = new BuyScreen(_popupWidth, _popupHeight);
            SellScreen = new SellScreen(_popupWidth, _popupHeight);

            if (Player.GreetMessages != null)
            {
                Random random = new Random();
                int i = random.Next(0, Player.GreetMessages.Length);
                MessageLog.Add($"{Player.GreetMessages[i]}", Colors.Gold);
            }
            else
            {
                MessageLog.Add("Good luck on your quest ", Colors.Gold);
            }
            // MessageLog.Add($"Level created with seed '{seed}'"); // debug

            _rootConsole.Update += OnIntroUpdate;
            _rootConsole.Render += OnIntroRender;

            Timer t = new Timer(UpdateColoredText, null, 0, 200);

            _rootConsole.Run();
        }

        private static void UpdateColoredText(object state)
        {
            if (i < 16 && goingForward == true)
            {
                i++;
            }
            else if (i == 16)
            {
                goingForward = false;
            }

            if (i > 0 && goingForward == false)
            {
                i--;
            }
            else if (i == 0)
            {
                goingForward = true;
            }

            float G = ((float)i * 8) / 255;
            float B = ((float)i * 8) / 255;

            IntroColor = new RLColor(1, G, B);
        }

        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            if (TargetingSystem.IsPlayerTargeting)
            {
                RLKeyPress keyPress = _rootConsole.Keyboard.GetKeyPress();
                if (keyPress != null)
                {
                    _renderRequired = true;
                    TargetingSystem.HandleKey(keyPress.Key);
                }
            }
            else if (CommandSystem.IsPlayerTurn)
            {
                if (InputSystem.GetInput(_rootConsole, CommandSystem))
                {
                    Player player = Game.Player;
                    if (player.Hunger <= 0 && player.Status == "Hungry")
                    {
                        player.Status = "Starving";
                        player.Hunger = 0;
                    }
                    else if (player.Hunger <= 50 && player.Hunger > 1 && (player.Status == "Healthy" || player.Status == "Starving"))
                    {
                        player.Status = "Hungry";
                    }
                    else if (player.Hunger > 50 && !(player.Status == "Poisoned" || player.Status == "Hardened"))
                    {
                        player.Status = "Healthy";
                    }
                    ++_steps; // hp
                    _renderRequired = true;
                    CommandSystem.EndPlayerTurn();
                }
            }
            else
            {
                _renderRequired = true;
                CommandSystem.ActivateMonsters();
            }
        }

        private static void OnRootConsoleUpdatePopupScreen(object sender, UpdateEventArgs e)
        {
            if (InputSystem.GetInput(_rootConsole, CommandSystem))
            {
                _renderRequired = true;
            }
        }

        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            if (_renderRequired)
            {
                _mapConsole.Clear();
                _statConsole.Clear();
                _messageConsole.Clear();
                _inventoryConsole.Clear();

                DungeonMap.Draw(_mapConsole, _statConsole);

                Player.Draw(_mapConsole, DungeonMap);
                Player.DrawStats(_statConsole);
                Player.DrawInventory(_inventoryConsole);

                MessageLog.Draw(_messageConsole);
                _messageConsole.SetBackColor(0, 0, _messageWidth, _messageHeight, Swatch.DbDark);
                TargetingSystem.Draw(_mapConsole);

                RLConsole.Blit(_mapConsole, 0, 0, _mapWidth, _mapHeight, _rootConsole, 0, _inventoryHeight);
                RLConsole.Blit(_statConsole, 0, 0, _statWidth, _statHeight, _rootConsole, _mapWidth, 0);
                RLConsole.Blit(_messageConsole, 0, 0, _messageWidth, _messageHeight, _rootConsole, 0, _screenHeight - _messageHeight);
                RLConsole.Blit(_inventoryConsole, 0, 0, _inventoryWidth, _inventoryHeight, _rootConsole, 0, 0);

                if (IsInventoryScreenShowing)
                {
                    InventoryScreen.Draw(_rootConsole, Player.Inventory);
                }
                if (IsBuyScreenShowing)
                {
                    BuyScreen.Draw(_rootConsole, Player.Inventory);
                }
                if (IsDialogScreenShowing)
                {
                    DialogScreen.Draw(_rootConsole);
                }
                if (IsShopSelectionScreenShowing)
                {
                    ShopSelectionScreen.Draw(_rootConsole);
                }
                if (IsBuyScreenShowing)
                {
                    BuyScreen.Draw(_rootConsole, Player.Inventory);
                }
                if (IsSellScreenShowing)
                {
                    SellScreen.Draw(_rootConsole, Player.Inventory);
                }

                _rootConsole.Draw();
                _renderRequired = false;
            }
        }

        public static bool AttemptMoveDownDungeonLevel()
        {
            if (DungeonMap.CanMoveDownToNextLevel())
            {
                MoveDownDungeonLevel();
            }

            return false;
        }

        public static bool MoveDownDungeonLevel()
        {
            DungeonMap.SetIsWalkable(Player.X, Player.Y, true);
            MapLevel += 1;

            if (MapLevel > DungeonMaps.Count)
            {
                MapGenerator mapGenerator = new MapGenerator(_mapWidth, _mapHeight, 20, 13, 7, MapLevel);
                _rootConsole.Title = $"RoguelikeGame - Level {MapLevel}";
                DungeonMaps.Add(mapGenerator.CreateMap());
                DungeonMap = DungeonMaps[MapLevel - 1];
            }
            else
            {
                _rootConsole.Title = $"RoguelikeGame - Level {MapLevel}";
                DungeonMap = DungeonMaps[MapLevel - 1];
                DungeonMap.PlacePlayerNearEntrance();
                DungeonMap.RescheduleExistingActors();
            }

            MessageLog = new MessageLog();
            CommandSystem = new CommandSystem();
            return true;
        }

        public static bool AttemptMoveUpDungeonLevel()
        {
            if (DungeonMap.CanMoveUpToNextLevel())
            {
                if (MapLevel - 1 == 0)
                {
                    MessageLog.Add("Coward", Swatch.DbBlood);
                }
                else
                {
                    MapLevel -= 1;
                    MessageLog = new MessageLog();
                    CommandSystem = new CommandSystem();
                    DungeonMap = DungeonMaps[MapLevel - 1];
                    DungeonMap.SetIsWalkable(Player.X, Player.Y, true);
                    DungeonMap.PlacePlayerNearExit();
                    DungeonMap.RescheduleExistingActors();
                    _rootConsole.Title = $"RoguelikeGame - Level {MapLevel}";
                }
                return true;
            }
            return false;
        }

        public static void TogglePopupScreen()
        {
            if (IsInventoryScreenShowing || IsDialogScreenShowing || IsShopSelectionScreenShowing || IsBuyScreenShowing || IsSellScreenShowing)
            {
                InputSystem = new InputSystemPopupScreen();
                _rootConsole.Update -= OnRootConsoleUpdate;
                _rootConsole.Update += OnRootConsoleUpdatePopupScreen;
                _renderRequired = true;
            }
            else
            {
                InputSystem = new InputSystemPlaying();
                _rootConsole.Update -= OnRootConsoleUpdatePopupScreen;
                _rootConsole.Update += OnRootConsoleUpdate;
                _renderRequired = true;
            }
        }

        public static void GameOver()
        {
            _rootConsole.Update -= OnRootConsoleUpdate;
            _rootConsole.Render -= OnRootConsoleRender;
            _rootConsole.Update += GameOverUpdate;
            _rootConsole.Render += GameOverRender;
        }

        public static double DistanceBetween(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        private static void OnIntroRender(object sender, UpdateEventArgs e)
        {
            if (_renderRequired)
            {
                _rootConsole.Clear();
                
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 7, (int)(_rootConsole.Height * 0.25) - 4,
                    "\xDA\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xBF", IntroColor // Swatch.DbBlood
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 7, (int)(_rootConsole.Height * 0.25) - 3,
                    "\xB3  RL GAME  \xB3", IntroColor
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 7, (int)(_rootConsole.Height * 0.25) - 2,
                    "\xB3           \xB3", IntroColor
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 7, (int)(_rootConsole.Height * 0.25) - 1,
                    "\xB3   ALPHA   \xB3", IntroColor
                );
                _rootConsole.Print( (int)(_rootConsole.Width * 0.5) - 7, (int)(_rootConsole.Height * 0.25),
                    "\xC0\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xD9", IntroColor
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 6, (int)(_rootConsole.Height * 0.5) + 2, 
                    "N - New Game", Swatch.DbSun
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 6, (int)(_rootConsole.Height * 0.5) + 4,
                    "L - Lore", Swatch.DbSun
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 6, (int)(_rootConsole.Height * 0.5) + 6, 
                    "E - Exit Game", Swatch.DbBlood
                );
                _rootConsole.Draw();
            }
        }

        private static void OnIntroUpdate(object sender, UpdateEventArgs e)
        {
            RLKeyPress introKeyPress = _rootConsole.Keyboard.GetKeyPress();
            if (introKeyPress != null)
            {
                if (introKeyPress.Key == RLKey.N)
                {
                    _rootConsole.Update -= OnIntroUpdate;
                    _rootConsole.Render -= OnIntroRender;
                    _rootConsole.Update += OnRootConsoleUpdate;
                    _rootConsole.Render += OnRootConsoleRender;
                }
                else if (introKeyPress.Key == RLKey.L)
                {
                    _rootConsole.Update -= OnIntroUpdate;
                    _rootConsole.Render -= OnIntroRender;
                    _rootConsole.Update += OnStoryUpdate;
                    _rootConsole.Render += OnStoryRender;
                }
                else if (introKeyPress.Key == RLKey.E)
                {
                    _rootConsole.Close();
                }
            }
            else
            {
                _renderRequired = true;
            }
        }

        private static void GameOverRender(object sender, UpdateEventArgs e)
        {
            if (_renderRequired)
            {
                _rootConsole.Clear();
                _rootConsole.Print(
                    (int)(_rootConsole.Width * 0.5) - 7,
                    (int)(_rootConsole.Height * 0.25) - 4,
                    "\xDA\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xBF",
                    IntroColor // Swatch.DbGrass
                );
                _rootConsole.Print(
                    (int)(_rootConsole.Width * 0.5) - 7,
                    (int)(_rootConsole.Height * 0.25) - 3,
                    "\xB3    WOW    \xB3",
                    IntroColor
                );
                _rootConsole.Print(
                    (int)(_rootConsole.Width * 0.5) - 7,
                    (int)(_rootConsole.Height * 0.25) - 2,
                    "\xB3           \xB3",
                    IntroColor
                );
                _rootConsole.Print(
                    (int)(_rootConsole.Width * 0.5) - 7,
                    (int)(_rootConsole.Height * 0.25) - 1,
                    "\xB3 YOUR DEAD \xB3",
                    IntroColor
                );
                _rootConsole.Print(
                    (int)(_rootConsole.Width * 0.5) - 7,
                    (int)(_rootConsole.Height * 0.25),
                    "\xC0\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xD9",
                    IntroColor
                );
                _rootConsole.Print(
                    (int)(_rootConsole.Width * 0.5) - 6,
                    (int)(_rootConsole.Height * 0.5) + 2,
                    "T - Try Again (Cheater)",
                    Swatch.DbBlood // previously: Swatch.DbSun
                );
                _rootConsole.Print(
                    (int)(_rootConsole.Width * 0.5) - 6,
                    (int)(_rootConsole.Height * 0.5) + 4,
                    "E - Exit Game",
                    Swatch.DbBlood // previously: Swatch.DbSun
                );
                _rootConsole.Draw();
            }
        }

        private static void GameOverUpdate(object sender, UpdateEventArgs e)
        {
            RLKeyPress introKeyPress = _rootConsole.Keyboard.GetKeyPress();
            if (introKeyPress != null)
            {
                if (introKeyPress.Key == RLKey.T)
                {
                    Game.MessageLog.Add("You Cheater! You were resurrected", Swatch.DbBlood);
                    Player.Health = Player.MaxHealth;
                    Player.Hunger = 1200;
                    IsGameOver = false;
                    _rootConsole.Update -= GameOverUpdate;
                    _rootConsole.Render -= GameOverRender;
                    _rootConsole.Update += OnRootConsoleUpdate;
                    _rootConsole.Render += OnRootConsoleRender;
                }
                else if (introKeyPress.Key == RLKey.E)
                {
                    _rootConsole.Close();
                }
            }
            else
            {
                _renderRequired = true;
            }
        }

        private static void OnStoryUpdate(object sender, UpdateEventArgs e)
        {
            RLKeyPress storyKeyPress = _rootConsole.Keyboard.GetKeyPress();
            if (storyKeyPress != null)
            {
                if (storyKeyPress.Key == RLKey.B)
                {
                    _rootConsole.Update -= OnStoryUpdate;
                    _rootConsole.Render -= OnStoryRender;
                    _rootConsole.Update += OnIntroUpdate;
                    _rootConsole.Render += OnIntroRender;
                }
            }
            else
            {
                _renderRequired = true;
            }
        }

        private static void OnStoryRender(object sender, UpdateEventArgs e)
        {
            if (_renderRequired)
            {
                _rootConsole.Clear();
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) - 10,
                    "\xDA\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4" +
                    "\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4" +
                    "\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4" +
                    "\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xBF",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) - 9,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) - 8,
                    "\xB3                        --- LORE SHIT GOES HERE ---                           \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) - 7,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) - 6,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) - 5,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) - 4,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) - 3,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) - 2,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40,  (int)(_rootConsole.Height * 0.25) - 1,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25),
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 1,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 2,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 3,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 4,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 5,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 6,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 7,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40,  (int)(_rootConsole.Height * 0.25) + 8,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 9,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 10,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 11,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 12,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 13,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 14,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 15,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 16,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 17,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 18,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 19,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 20,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 21,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 22,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 23,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 24,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 25,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 26,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 27,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 28,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 29,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 30,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 31,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 32,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 33,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 34,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 35,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 36,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 37,
                    "\xB3                                                                              \xB3",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.5) - 40, (int)(_rootConsole.Height * 0.25) + 38,
                    "\xC0\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4" +
                    "\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4" +
                    "\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4" +
                    "\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xC4\xD9",
                    Swatch.DbGrass
                );
                _rootConsole.Print((int)(_rootConsole.Width * 0.25) - 14, (int)(_rootConsole.Height * 0.25) + 42,
                    "B - Go Back",
                    Swatch.DbBlood // previously: Swatch.DbSun
                );
                _rootConsole.Draw();
            }
        }
    }
}
