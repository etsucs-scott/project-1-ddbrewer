using System.Collections.Generic;

namespace AdventureGame.Core
{
    public class Player : ICharacter
    {
        public int Health { get; private set; }
        public int MaxHealth { get; } = 150;
        public int AttackPower => 10 + BestWeaponModifier;
        public bool IsAlive => Health > 0;

        public List<Weapon> Weapons { get; } = new List<Weapon>();
        public int BestWeaponModifier { get; private set; }

        public Player()
        {
            Health = 100;
            BestWeaponModifier = 0;
        }

        public void PickUpWeapon(Weapon weapon)
        {
            if (weapon == null)
                return;
            
            Weapons.Add(weapon);

            if (weapon.AttackModifier > BestWeaponModifier)
                BestWeaponModifier = weapon.AttackModifier;
        }

        public void Attack(ICharacter target)
        {
            if (!IsAlive || target == null || !target.IsAlive)
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

        public void Heal(int amount)
        {
            if (amount <= 0 || !IsAlive)
                return;

            Health += amount;

            if (Health > MaxHealth)
                Health = MaxHealth;
        }
    }
}