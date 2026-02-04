namespace AdventureGame.Core
{
    public class Weapon : Item
    {
        public int AttackModifier;
        public Weapon()
        {
            Name = "Sword";

            PickUpMessage = "You picked up the sword.";
            
            AttackModifier = 10;
        }
    }
}