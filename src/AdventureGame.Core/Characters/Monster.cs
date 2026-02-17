namespace AdventureGame.Core
{
    public class Monster : ICharacter
    {
        private static Random random = new Random(); // Added to allow Monster to have a random hp value from 30-50.

        public int Health { get; private set; }
        public int AttackPower { get; } = 10;
        public bool IsAlive => Health > 0;

        public Monster()
        {
            Health = random.Next(30, 51);
        }

        public void Attack(ICharacter target)
        {
            if (!IsAlive || !target.IsAlive)
                return;

            target.TakeDamage(AttackPower);
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0)
                return;

            Health -= amount;

            if (Health < 0)
                Health = 0;
        }
    }
}