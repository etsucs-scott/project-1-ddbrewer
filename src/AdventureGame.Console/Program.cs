using System;
using AdventureGame.Core;

class Program // To sanity check: dotnet run --project src/AdventureGame.Console
{
    static void Main()
    {
        Random rng = new Random();

        Position start = new Position(0, 0);
        Position exit = new Position(9, 9);

        Maze maze = MazeGenerator.Generate(
            width: 10,
            height: 10,
            start: start,
            exit: exit,
            wallPercent: 25,
            potionCount: 3,
            weaponCount: 3,
            monsterCount: 3,
            rng: rng
        );

        Player player = new Player();
        GameEngine engine = new GameEngine(maze, player, start);
        engine.Run();
    }
}