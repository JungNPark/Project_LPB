using UnityEngine;
using System.Collections.Generic;

//TODO: 런타임에서 Effect들을 가져오는 것이 아니라 미리 Effect를 가져온 뒤 프리팹에 저장해두는 방식을 고민해보자.
public class Item : MonoBehaviour, IItem
{
    #region Variables

    private List<IAttackEffect> _attackEffects = new List<IAttackEffect>();
    private List<IAcquireEffect> _acquireEffects = new List<IAcquireEffect>();
    private List<ICollisionEffect> _collisionEffects = new List<ICollisionEffect>();
    private List<IHitEffect> _hitEffects = new List<IHitEffect>();
    private List<IItemEffect> _itemEffects = new List<IItemEffect>();
    private List<ITickEffect> _tickEffects = new List<ITickEffect>();
    private List<IUpdateEffect> _updateEffects = new List<IUpdateEffect>();

    #endregion
    
    #region Properties

    public List<IAttackEffect> AttackEffects { get=>_attackEffects; }
    public List<IAcquireEffect> AcquireEffects { get=>_acquireEffects; }
    public List<ICollisionEffect> CollisionEffects { get=>_collisionEffects; }
    public List<IHitEffect> HitEffects { get=>_hitEffects; }
    public List<IItemEffect> ItemEffects { get=>_itemEffects; }
    public List<ITickEffect> TickEffects { get=>_tickEffects; }
    public List<IUpdateEffect> UpdateEffects{ get=>_updateEffects; }

    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    #region Public Methods
    public void InitItemEffects()
    {
        if(_itemEffects.Count != 0)
        {
            return;
        }
        GetComponents(_itemEffects);
        foreach (IItemEffect effect in _itemEffects)
        {
            if (effect is IAttackEffect attackEffect)
            {
                _attackEffects.Add(attackEffect);
            }
            if (effect is IAcquireEffect acquireEffect)
            {
                _acquireEffects.Add(acquireEffect);
            }
            if (effect is ICollisionEffect collisionEffect)
            {
                _collisionEffects.Add(collisionEffect);
            }
            if (effect is IHitEffect hitEffect)
            {
                _hitEffects.Add(hitEffect);
            }
            if (effect is ITickEffect tickEffect)
            {
                _tickEffects.Add(tickEffect);
            }
            if (effect is IUpdateEffect updateEffect)
            {
                _updateEffects.Add(updateEffect);
            }
        }
    }

    public void OnAcquire(IBall ball)
    {
    
    }

    public void OnRelease(IBall ball)
    {
        
    }

    #endregion

}
