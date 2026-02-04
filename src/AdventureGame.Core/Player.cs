using System.Collections.Generic;

namespace AdventureGame.Core
{
    public class Player : ICharacter
    {
        public int Health {get; set;} = 100;
        public int MaxHealth {get; set;} = 150;

        public List<Item> Inventory = new List<Item>();
         public Weapon EquippedWeapon = null;

        public bool Alive
        {
            get {return Health > 0;}
        }

        public int Attack()
        {
            if (EquippedWeapon == null)
                return 1;
            return 1 + EquippedWeapon.AttackModifier;
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
        }

        public void GetItem(Item item)
        {
            if (item is Potion)
            {
                Health += 20;
                if (Health > MaxHealth)
                Health = MaxHealth;
            }
            else if (item is Weapon)
            {
                EquippedWeapon = (Weapon)item;
                Inventory.Add(item);
            }
        }
        }
    }