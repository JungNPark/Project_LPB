using System.Collections.Generic;
using UnityEngine;

public class AcquireEffect_Penetration : MonoBehaviour, IAcquireEffect
{
    public void OnAcquire(IBall ball)
    {
        int ballLayer = LayerMask.NameToLayer("Ball");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics.IgnoreLayerCollision(ballLayer, enemyLayer, true);

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

}