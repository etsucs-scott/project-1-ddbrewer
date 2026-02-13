namespace AdventureGame.Core
{
    public class Maze
    {
        public int Width = 10;
        public int Height = 10;
        public Tile[,] Tiles;

        public int startX = 1;
        public int startY = 1;

        public int exitX = 8;
        public int exitY = 8;

        public Maze()
        {
            Tiles = new Tile[Width, Height];

            // Creates tiles and sets them all to walls
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Tiles[x, y] = new Tile();
                    Tiles[x, y].Type = TileType.Wall;
                }
            }

            // Creates a very simple path to the exit
            for (int x = startX; x <= exitX; x++)
                Tiles[x, startY].Type = TileType.Empty;
            
            for (int y = startY; y<= exitY; y++)
                Tiles[exitX, y].Type = TileType.Empty;

            // Places the exit
            Tiles[exitX, exitY].Type = TileType.Exit;
        }
    }
}