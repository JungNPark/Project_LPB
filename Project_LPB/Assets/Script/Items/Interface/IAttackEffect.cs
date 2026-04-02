using UnityEngine;

public interface IAttackEffect : IItemEffect
{
    void OnAttack(IUnit Attacker, IUnit target, float damage);
}
