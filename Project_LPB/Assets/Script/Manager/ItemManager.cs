using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Item[] testItems;
    [SerializeField]
    private BallBase testBall;
    [SerializeField]
    private int[] AcquireNumberList;
    private List<IAttackEffect> attackEffects = new List<IAttackEffect>();
    private List<IHitEffect> hitEffects = new List<IHitEffect>();

    #endregion

    #region Properties

    #endregion

    #region Unity LifeCycle
    void Start()
    {
        foreach(int AcquireNumber in AcquireNumberList)
        {
            if(AcquireNumber >= testItems.Length || AcquireNumber < 0)
            {
                Debug.Log("AcquireNumber가 정해진 범위를 초과했습니다.");
            }
            AcquireItem(testItems[AcquireNumber], testBall);
        }
        testBall.attackDelegate += ExecuteAttackEffect;
    }

    #endregion

    #region Public Methods
    public void AcquireItem(Item item, IBall ball)
    {
        item.InitItemEffects();
        //아이템의 획득시 효과 모두 실행
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
        Debug.Log($"Execute Attacker Effect - damage : {damage}");
        foreach(var attackEffect in attackEffects)
        {
            attackEffect.OnAttack(attacker, target , damage);
        }
    }

    #endregion

    #region Private Methods

    #endregion


}
