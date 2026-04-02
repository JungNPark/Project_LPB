using System.Collections.Generic;
using UnityEngine;

public class AcquireEffect_ExecuteGeneralEffect : MonoBehaviour, IAcquireEffect
{
    [SerializeField]
    private int _targetGeneralID = 0;

    private List<IGeneralEffect> targetGeneralEffects = new List<IGeneralEffect>();
    public void OnAcquire(IBall ball)
    {
        if(targetGeneralEffects.Count == 0)
        {
            FetchGeneralEffects();
        }
        foreach(var generalEffect in targetGeneralEffects)
        {
            generalEffect.Execute(ball);
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