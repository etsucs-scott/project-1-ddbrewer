namespace AdventureGame.Core
{
    public class Tile
    {
        public TileType Type { get; private set; }
        public Item Item { get; private set; } // Potion / Weapon / Empty space
        public Monster Monster { get; private set; } // Monster / Empty space

        public bool IsWalkable => Type !=TileType.Wall;

        public Tile(TileType type = TileType.Floor)
        {
            Type = type;
        }

        public void SetType(TileType type) => Type = type;

        public void PlaceItem(Item item) => Item = item;
        public Item TakeItem()
        {
            Item taken = Item;
            Item = null;
            return taken;
        }

        public void PlaceMonster(Monster monster) => Monster = monster;
        public Monster RemoveMonster()
        {
            Monster removed = Monster;
            Monster = null;
            return removed;
        }
    }
}