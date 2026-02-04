namespace AdventureGame.Core
{
    public class Potion : Item
    {
        public int Heal;
        public  Potion()
        {
            Name = "Potion";

            PickUpMessage = "You picked up the potion.";
            
            Heal = 20;
        }
    }
}