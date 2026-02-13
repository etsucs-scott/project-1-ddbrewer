using System;
using System.Collections.Generic;

namespace AdventureGame.Core
{
    public class Monster : ICharacter
    {
        public int Health {get; private set;} = 30;
        
        public bool Alive
        {
            get {return Health > 0;}
        }

        public int Attack()
        {
            return 10;
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
        }
    }
}