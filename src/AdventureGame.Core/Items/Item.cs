namespace AdventureGame.Core
{
    public abstract class Item
    {
        public string Name { get; }
        public string PickUpMessage { get; }

        protected Item(string name, string pickupMessage)
        {
            Name = name;
            PickUpMessage = pickupMessage;
        }
    }
}