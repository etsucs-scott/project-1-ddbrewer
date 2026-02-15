namespace AdventureGame.Core
{
    public class Monster : ICharacter
    {
        private static Random random = new Random();
        
        public int Health { get; private set; }
        public int AttackPower { get; private set; }
        public bool IsAlive => Health > 0;

        public Monster(int health, int attackPower)
        {
            Health = random.Next(30, 51);
            AttackPower = attackPower;
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