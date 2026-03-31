using UnityEngine;
using System.Collections.Generic;
using System;

public interface IItem
{
    public List<IAttackEffect> AttackEffects { get; }
    public List<IAcquireEffect> AcquireEffects { get; }
    public List<ICollisionEffect> CollisionEffects { get; }
    public List<IHitEffect> HitEffects { get; }
    public List<IItemEffect> ItemEffects { get; }
    public List<ITickEffect> TickEffects { get; }
    public List<IUpdateEffect> UpdateEffects{ get; }
    public void OnAcquire(IBall ball);
    public void OnRelease(IBall ball);
}
