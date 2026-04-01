using UnityEngine;

public interface IUnit
{
    Stat Stat{get; set;}
    float TakeDamage(IUnit attacker, float damage);
    
}
