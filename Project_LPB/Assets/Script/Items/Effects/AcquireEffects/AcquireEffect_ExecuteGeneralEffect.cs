using System.Collections.Generic;
using UnityEngine;

public class AcquireEffect_ExecuteGeneralEffect : MonoBehaviour, IAcquireEffect
{
    #region Variables
    //호출할 GeneralEffect를 담는 리스트
    [SerializeField]
    private List<IGeneralEffect> targetGeneralEffects = new List<IGeneralEffect>();
    //이 ExecuteGeneralEffect의 타겟이 되는 GeneralEffect의 ID
    private int _targetGeneralID = 0;
    #endregion

    #region Properties

    #endregion

    #region Unity LifeCycle

    #endregion

    #region Public Methods
    public void OnAcquire(IBall ball)
    {
        //실행할 General Effect가 없을 경우 컴포넌트에서 가져온다.
        if(targetGeneralEffects.Count == 0)
        {
            FetchGeneralEffects();
        }
        //등록된 모든 General Effect를 실행한다.
        foreach(var generalEffect in targetGeneralEffects)
        {
            generalEffect.Execute(ball);
        }
    }

    #endregion

    #region Private Methods
    /// <summary>
    /// 현재 오브젝트의 컴포넌트 중 GeneralID가 일치하는 General Effect를 가져오는 함수
    /// </summary>
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