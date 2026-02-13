using System.Dynamic;

namespace AdventureGame.Core
{
    public interface ICharacter
{
    int Health {get;}

    bool Alive {get;}

    int Attack();

    void TakeDamage(int amount);
}
}