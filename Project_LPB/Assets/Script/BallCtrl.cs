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
}
