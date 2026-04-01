using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] testItems;
    public BallBase testBall;
    public int[] AcquireNumberList;
    public List<IAttackEffect> attackEffects;
    public List<IHitEffect> hitEffects;

    void Start()
    {
        foreach(int AcquireNumber in AcquireNumberList)
        {
            AcquireItem(testItems[AcquireNumber], testBall);
        }
        testBall.attackDelegate += ExecuteAttackEffect;
    }

    public void AcquireItem(Item item, IBall ball)
    {
        item.InitItemEffects();
        foreach(var acquireEffect in item.AcquireEffects)
        {
            acquireEffect.OnAcquire(ball);
        }
        //item에 있는 모든 IAttackEffect 가져오기
        foreach(var attackEffect in item.AttackEffects)
        {
            attackEffects.Add(attackEffect);
        }
        //item에 있는 모든 IHitEffect 가져오기
        foreach(var hitEffect in item.HitEffects)
        {
            hitEffects.Add(hitEffect);
        }
    }

    public void ExecuteAttackEffect(IUnit attacker, IUnit target, float damage)
    {
        Debug.Log("Execute Attack Effect");
    }
}
