using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public struct BallStat
{
    public Vector2 dir;
    public float Speed;
}

[RequireComponent(typeof(Rigidbody))]
public class BallCtrl : MonoBehaviour
{
    //
    private Rigidbody rb;
    public BallStat stat;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        
    }

    public void Shoot(BallStat stat)
    {
        Vector3 dir = new Vector3(stat.dir.x, 0, stat.dir.y);
        dir = dir.normalized;
        rb.linearVelocity = dir * stat.Speed;
    }

    public void Shoot(Vector2 dir)
    {
        stat.dir = dir;
        Shoot(stat);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 어딘가에 충돌하여 튕겨나가는(물리 엔진에 의해 계산된) 속도를 가져와서 
        // 새로운 2D 진행 방향(X, Z)을 계산하고 stat.dir에 반영합니다.
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            stat.dir = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).normalized;
        }
    }
}
