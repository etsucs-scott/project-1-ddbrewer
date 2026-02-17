namespace AdventureGame.Core
{
    public class Potion : Item
    {
        private const int HealAmount = 20;

        public Potion()
            :base("Health Potion", "You picked up the potion! You're feeling invigorated!") { }

        public void Use(Player player)
        {
            if (player == null || !player.IsAlive)
                return;

            player.Heal(HealAmount);
        }
    }
}