using Unity.Profiling;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public struct BallStat
{
    public Vector2 dir;
    public float speed;
    public float damage;
    public float size;
}

[RequireComponent(typeof(Rigidbody))]
public class BallCtrl : MonoBehaviour
{
    //
    private Rigidbody rb;
    public BallStat stat;
    public float SpeedUpScale = 1.0f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        ApplyStat();
    }

    private void ApplyStat()
    {
        rb.linearVelocity = rb.linearVelocity.normalized * stat.speed;
        transform.localScale = Vector3.one * stat.size;
    }

    public void Shoot(BallStat stat)
    {
        Vector3 dir = new Vector3(stat.dir.x, 0, stat.dir.y);
        dir = dir.normalized;
        rb.linearVelocity = dir * stat.speed;
    }

    public void Shoot(Vector2 dir)
    {
        stat.dir = dir;
        Shoot(stat);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            stat.dir = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).normalized;
        }
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.OnDamaged(stat.damage);
            }
        }
    }
}
