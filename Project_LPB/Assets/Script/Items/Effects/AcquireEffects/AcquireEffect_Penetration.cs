using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 획득시 공에 관통효과를 추가하는 아이템 효과
/// </summary>
public class AcquireEffect_Penetration : MonoBehaviour, IAcquireEffect
{
    #region Variables

    #endregion
    
    #region Properties

    #endregion

    #region Unity LifeCycle

    #endregion

    #region Public Methods
    public void OnAcquire(IBall ball)
    {
        //공이 적을 통과하도록 레이어 충돌 변경
        int ballLayer = LayerMask.NameToLayer("Ball");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics.IgnoreLayerCollision(ballLayer, enemyLayer, true);

        //공이 공격 판정을 Trigger로 하도록 활성화된 콜라이더를 자식으로 추가 
        MonoBehaviour ballMono = ball as MonoBehaviour;
        if (ballMono != null)
        {
            GameObject triggerObj = new GameObject("PenetrationTrigger");
            triggerObj.transform.SetParent(ballMono.transform);
            triggerObj.transform.localPosition = Vector3.zero;

            SphereCollider sphereCollider = triggerObj.AddComponent<SphereCollider>();
            sphereCollider.radius = 0.5f;
            sphereCollider.isTrigger = true;
        }
    }

    #endregion

    #region Private Methods

    #endregion

}