using UnityEngine;

public interface IHitAble
{
    void OnHit(float damage, IUnit attacker);
}
