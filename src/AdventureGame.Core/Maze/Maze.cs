using System;

namespace AdventureGame.Core
{
    public class Maze
    {
        public int Width { get; }
        public int Height { get; }

        public Tile[,] Tiles { get; }

        public Maze(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Maze dimensions must be positive.");
            
            Width = width;
            Height = height;

            Tiles = new Tile[width, height];

            // Default all tiles to floor
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tiles[x, y] = new Tile(TileType.Floor);
                }
            }
        }

        public bool InBounds(int x, int y)
            => x >= 0 && x < Width && y >= 0 && y < Height;
        
        public Tile GetTile(int x, int y)
        {
            if (!InBounds(x, y))
                throw new ArgumentOutOfRangeException("Tile coordinates are out of bounds.");
            return Tiles[x, y];
        }

        public bool IsWalkable(int x, int y)
            => InBounds(x, y) && Tiles[x, y].IsWalkable;
        
        public void SetWall(int x, int y)
        {
            GetTile(x, y).SetType(TileType.Wall);
            GetTile(x, y).PlaceItem(null); // So walls can't hold items.
            GetTile(x, y).PlaceMonster(null); // Or monsters.
        }

        public void SetExit(int x, int y)
        {
            GetTile(x, y).SetType(TileType.Exit);
        }

        public void PlaceItem(int x, int y, Item item)
        {
            Tile tile = GetTile(x, y);
            if (!tile.IsWalkable)
                throw new InvalidOperationException("Cannot place items on walls.");
            tile.PlaceItem(item);
        }

        public void PlaceMonster(int x, int y, Monster monster)
        {
            Tile tile = GetTile(x, y);
            if (!tile.IsWalkable)
                throw new InvalidOperationException("Cannot place monsters on walls.");
            tile.PlaceMonster(monster);
        }
    }
}