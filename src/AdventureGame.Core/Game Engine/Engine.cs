using System;

namespace AdventureGame.Core
{
    public class GameEngine
    {
        private readonly Maze _maze;
        private readonly Player _player;

        public Position PlayerPosition { get; private set; }

        public GameEngine(Maze maze, Player player, Position startPosition)
        {
            _maze = maze;
            _player = player;
            PlayerPosition = startPosition;
        }

        public void Run()
        {
            Console.CursorVisible = false;

            while (!_gameOver)
            {
                DrawMaze();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                    break;
                
                TryMoveFromKey(keyInfo);
            }

            DrawMaze();
            Console.WriteLine();
            Console.WriteLine("Press any key to close the game...");
            Console.ReadKey(true);
        }

        private void TryMoveFromKey(ConsoleKeyInfo keyInfo)
        {
            int dx = 0;
            int dy = 0;

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow: dy = -1; break;
                case ConsoleKey.DownArrow: dy = 1; break;
                case ConsoleKey.LeftArrow: dx = -1; break;
                case ConsoleKey.RightArrow: dx = 1; break;

                case ConsoleKey.W: dy = -1; break;
                case ConsoleKey.S: dy = 1; break;
                case ConsoleKey.A: dx = -1; break;
                case ConsoleKey.D: dx = 1; break;

                default:    // Ignores other keys
                    return;
            }

            Position next = PlayerPosition.Move(dx, dy);

            // Out of bounds error
            if (!_maze.InBounds(next.X, next.Y))
            {
                ShowMessage("You can't go that way.");
                return;
            }

            // Wall error
            if (!_maze.IsWalkable(next.X, next.Y))
            {
                ShowMessage("You bumped into a wall.");
                return;
            }

            // Valid movement
            PlayerPosition = next;
            HandleTileEntered(next.X, next.Y);
        }

        private void HandleTileEntered(int x, int y) // Checks tiles for exit, monster, or item
        {
            Tile tile = _maze.GetTile(x, y);

            // Exit
            if (tile.Type == TileType.Exit)
            {
                _gameOver = true;
                _win = true;
                ShowMessage("You found the exit!");
                return;
            }

            // Combat
            if (tile.Monster != null && tile.Monster.IsAlive)
            {
                HandleCombat(tile);

                if (_gameOver)
                    return;
            }

            // Item pickup
            if (tile.Item != null)
            {
                Item item = tile.TakeItem();

                // Potion: ON USE PICKUP
                if (item is Potion potion)
                {
                    potion.Use(_player);
                    ShowMessage(item.PickUpMessage);
                }

                // Weapon: store in inventory (list), highest mod applies
                else if (item is Weapon weapon)
                {
                    _player.PickUpWeapon(weapon);
                    ShowMessage(item.PickUpMessage);
                }
            }
        }

        private void HandleCombat(Tile tile)
        {
            Monster monster = tile.Monster;
            if (monster == null || !monster.IsAlive)
                return;
            
            ShowMessage("A monster attacks!");

            while (_player.IsAlive && monster.IsAlive)
            {
                // Player turn
                _player.Attack(monster);
                if (!monster.IsAlive)
                    break;
                
                // Monster turn
                monster.Attack(_player);
            }

            if (!monster.IsAlive)
            {
                tile.RemoveMonster();
                ShowMessage("You defeated the monster!");
            }

            if (!_player.IsAlive)
            {
                _gameOver = true;
                _win = false;
                ShowMessage("You died...");
            }
        }

        private void DrawMaze()
        {
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < _maze.Height; y++)
            {
                for (int x = 0; x < _maze.Width; x++)
                {
                    if (PlayerPosition.X == x && PlayerPosition.Y == y)
                    {
                        Console.Write('@'); // Shows where Player is in the maze
                    }
                    else
                    {
                        Tile tile = _maze.GetTile(x, y);

                        if (tile.Type == TileType.Wall) Console.Write('#');
                        else if (tile.Type == TileType.Exit) Console.Write('E');
                        else if (tile.Monster != null && tile.Monster.IsAlive) Console.Write('M');
                        else if (tile.Item is Weapon) Console.Write('W');
                        else if (tile.Item is Potion) Console.Write('P');
                        else Console.Write('.');
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine($"HP: {_player.Health}/{_player.MaxHealth} || ATK: {_player.AttackPower}");
            Console.WriteLine("Movement: Arrow Keys or WASD || Quit: ESC");

            // Sloppy pickup item text fix:
            Console.WriteLine((_lastMessage ?? "").PadRight(Console.WindowWidth));
        }

        // To show pickup messages in the game
        private string _lastMessage = "";

        private void ShowMessage(string message)
        {
            _lastMessage = message;
        }
        
        private bool _gameOver;
        private bool _win;
    }
}