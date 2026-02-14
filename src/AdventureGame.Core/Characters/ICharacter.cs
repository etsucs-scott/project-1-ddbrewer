namespace AdventureGame.Core
{
    public interface ICharacter
    {
        string Name { get; }
        int Health { get; }
        int AttackPower { get; }
        bool IsAlive { get; }

        void Attack (ICharacter target);
        void TakeDamage (int amount);
    }
}