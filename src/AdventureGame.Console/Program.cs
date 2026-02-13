using System;
using AdventureGame.Core;

class Program
{
    static void Main()
    {
        Maze m = new Maze();

        for (int y = 0; y < m.Height; y++)
        {
            for (int x = 0; x < m.Width; x++)
            {
                TileType t = m.Tiles[x, y].Type;

                if (x == m.startX && y == m.startY)
                {
                    Console.Write('@');
                }
                else if (t == TileType.Wall)
                {
                    Console.Write('#');
                }
                else if (t == TileType.Empty)
                {
                    Console.Write('.');
                }
                else if (t == TileType.Exit)
                {
                     Console.Write('E');
                }
            }

            Console.WriteLine();
        }

        Console.ReadKey();
    }
}