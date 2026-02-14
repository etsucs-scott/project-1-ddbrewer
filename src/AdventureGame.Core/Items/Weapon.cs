namespace AdventureGame.Core
{
    public class Weapon : Item
    {
        public int AttackModifier { get; }

        public Weapon(string name, string pickupMessage, int attackModifier)
            :base(name, pickupMessage)
        {
            AttackModifier = attackModifier;
        }
    }
}