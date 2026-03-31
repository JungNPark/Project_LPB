using UnityEngine;

public class TestBall : BallBase
{
    public float SpeedUpScale = 1.0f;
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            _ballStat.dir = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).normalized;
        }
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.OnDamaged(_ballStat.damage);
            }
        }
    }
}