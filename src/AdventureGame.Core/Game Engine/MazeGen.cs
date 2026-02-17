using System;
using System.Collections.Generic;

namespace AdventureGame.Core
{
    public static class MazeGenerator
    {
        public static Maze Generate(
            int width,
            int height,
            Position start,
            Position exit,
            int wallPercent,
            int potionCount,
            int weaponCount,
            int monsterCount,
            Random rng)
        {
            if (rng == null) throw new ArgumentNullException(nameof(rng));

            while (true)
            {
                Maze maze = new Maze(width, height);

                // Adds walls
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if ((x == start.X && y == start.Y) || (x == exit.X && y == exit.Y))
                            continue;
                        
                        int roll = rng.Next(0, 100);
                        if (roll < wallPercent)
                            maze.SetWall(x, y);
                    }
                }

                // Adds exit
                maze.GetTile(exit.X, exit.Y).SetType(TileType.Exit);

                // Validates there is a path that works
                if (!PathExists(maze, start, exit))
                    continue;
                
                // Adds items, monsters, and empty tiles
                PlacePotions(maze, start, exit, potionCount, rng);
                PlaceWeapons(maze, start, exit, weaponCount, rng);
                PlaceMonsters(maze, start, exit, monsterCount, rng);

                return maze;
            }
        }

        private static void PlacePotions(Maze maze, Position start, Position exit, int count, Random rng)
        {
            for (int i = 0; i < count; i++)
            {
                Position p = FindRandomEmptyFloor(maze, start, exit, rng);
                maze.PlaceItem(p.X, p.Y, new Potion());
            }
        }

        private static void PlaceWeapons(Maze maze, Position start, Position exit, int count, Random rng)
        {
            for (int i = 0; i < count; i++)
            {
                Position p = FindRandomEmptyFloor(maze, start, exit, rng);

                int roll = rng.Next(0, 3);

                Weapon weapon;

                // Sets each weapon to have a specific name, pickup message, and modifier
                switch(roll)
                {
                    case 0:
                        weapon = new Weapon("Dagger", "You picked up a Dagger.", 5);
                        break;
                    
                    case 1:
                        weapon = new Weapon("Sword", "You picked up a Sword.", 10);
                        break;
                    
                    default:
                        weapon = new Weapon("Battleaxe", "You picked up a Battleaxe.", 20);
                        break;
                }

                maze.PlaceItem(p.X, p.Y, weapon);
            }
        }

        private static void PlaceMonsters(Maze maze, Position start, Position exit, int count, Random rng)
        {
            for (int i = 0; i < count; i++)
            {
                Position p = FindRandomEmptyFloor(maze, start, exit, rng);
                maze.PlaceMonster(p.X, p.Y, new Monster());
            }
        }

        private static Position FindRandomEmptyFloor(Maze maze, Position start, Position exit, Random rng)
        {
            while (true)
            {
                int x = rng.Next(0, maze.Width);
                int y = rng.Next(0, maze.Height);

                if (x == start.X && y == start.Y) continue;
                if (x == exit.X && y == exit.Y) continue;

                Tile t = maze.GetTile(x, y);

                if (!t.IsWalkable) continue;
                if (t.Type != TileType.Floor) continue;
                if (t.Item != null) continue;
                if (t.Monster != null && t.Monster.IsAlive) continue;

                return new Position(x, y);
            }
        }

        private static bool PathExists(Maze maze, Position start, Position exit)
        {
            bool[,] visited = new bool[maze.Width, maze.Height];
            Queue<Position> q = new Queue<Position>();

            visited[start.X, start.Y] = true;
            q.Enqueue(start);

            while (q.Count > 0)
            {
                Position cur = q.Dequeue();

                if (cur.X == exit.X && cur.Y == exit.Y)
                    return true;

                TryEnqueue(maze, cur.X + 1, cur.Y, visited, q);
                TryEnqueue(maze, cur.X - 1, cur.Y, visited, q);
                TryEnqueue(maze, cur.X, cur.Y + 1, visited, q);
                TryEnqueue(maze, cur.X, cur.Y - 1, visited, q);
            }

            return false;
        }

        private static void TryEnqueue(Maze maze, int x, int y, bool[,] visited, Queue<Position> q)
        {
            if (!maze.InBounds(x, y)) return;
            if (visited[x, y]) return;
            if (!maze.IsWalkable(x, y)) return;

            visited[x, y] = true;
            q.Enqueue(new Position(x, y));
        }
    }
}