namespace AdventureGame.Core
{
    public struct Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position Move(int dx, int dy) => new Position(X + dx, Y + dy);
    }
}