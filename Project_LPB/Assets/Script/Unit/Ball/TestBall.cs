using UnityEngine;

public class TestBall : BallBase
{
    public float SpeedUpScale = 1.0f;
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            _ballStat.dir = rb.linearVelocity.normalized;
        }
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                ApplyDamage(enemy, _stat.damage.Value);
            }
        }
    }

    protected override void ApplyDamage(IUnit target, float damage)
    {
        base.ApplyDamage(target, damage);

        target.TakeDamage(this, damage);
    }
}