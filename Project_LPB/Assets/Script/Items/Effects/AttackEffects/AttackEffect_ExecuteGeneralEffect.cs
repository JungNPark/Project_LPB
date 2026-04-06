using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 적중시 범용 효과를 호출하는 클래스
/// </summary>
public class AttackEffect_ExecuteGeneralEffect : MonoBehaviour, IAttackEffect
{
    #region Variables
    
    //이 클래스가 작동시킬 범용 효과의 ID
    [SerializeField]
    private int _targetGeneralID;
    //이 클래스가 작동시킬 범용 효과 리스트
    private List<IGeneralEffect> targetGeneralEffects = new List<IGeneralEffect>();

    #endregion

    #region Public Methods
    public void OnAttack(IUnit attacker, IUnit target, float damage)
    {
        if(targetGeneralEffects.Count == 0)
        {
            FetchGeneralEffects();
        }
        foreach(var generalEffect in targetGeneralEffects)
        {
            generalEffect.Execute<float>(attacker, target, damage);
        }
    }

    #endregion

    #region Private Methods
    private void FetchGeneralEffects()
    {
        
        IGeneralEffect[] allGeneralEffects = GetComponents<IGeneralEffect>();
        foreach (IGeneralEffect effect in allGeneralEffects)
        {
            if (effect.GeneralID == _targetGeneralID)
            {
                targetGeneralEffects.Add(effect);
            }
        }
    }

    #endregion
}