using UnityEngine;

public interface IAttackEffect : IItemEffect
{
    void Execute(IUnit Attacker, IUnit target, IUnit damage);
}
