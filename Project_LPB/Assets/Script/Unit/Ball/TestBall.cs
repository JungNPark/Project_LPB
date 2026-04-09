using UnityEngine;

public class TestBall : BallBase
{
    #region Variables

    #endregion
    
    #region Properties

    #endregion

    #region Unity LifeCycle
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
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

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                ApplyDamage(enemy, _stat.damage.Value);
            }
        }
    }

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods
    protected override void ApplyDamage(IUnit target, float damage)
    {
        base.ApplyDamage(target, damage);

        target.TakeDamage(this, damage);
    }

    #endregion

}