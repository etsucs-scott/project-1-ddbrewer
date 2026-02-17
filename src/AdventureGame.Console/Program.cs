using System;
using AdventureGame.Core;

class Program // To sanity check: dotnet run --project src/AdventureGame.Console
{
    static void Main()
    {
        Maze maze = new Maze(10, 10);

        // Wall and Exit
        maze.SetExit(9, 9);
        maze.SetWall(3, 3);

        // Items
        maze.PlaceItem(2, 2, new Potion());
        maze.PlaceItem(4, 1, new Weapon("Sword", "You picked up the sword!", 10));

        // Monsters
        maze.PlaceMonster(5, 5, new Monster());

        Player player = new Player();

        GameEngine engine = new GameEngine(maze, player, new Position(0,0));
        engine.Run();
    }
}