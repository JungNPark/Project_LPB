using UnityEngine;
using System.Collections.Generic;

public class AttackEffect_ExecuteGeneralEffect : MonoBehaviour, IAttackEffect
{
    #region Variables

    #endregion
    #region Properties

    #endregion

    #region Unity LifeCycle

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

    #endregion
    [SerializeField]
    private int _targetGeneralID;
    private List<IGeneralEffect> targetGeneralEffects = new List<IGeneralEffect>();
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
}