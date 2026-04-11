using UnityEngine;
using System.Collections;

public class Enemy : UnitBase
{
    #region Variables
    private Renderer rend;
    private Color originalColor;

    #endregion

    #region Properties

    
    #endregion

    #region Unity LifeCycle

    void Start()
    {
        Stat.nowHp = Stat.maxHp.Value;
        rend = GetComponent<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    #endregion

    #region Public Methods
    public void Attack(IUnit target)
    {
        ApplyDamage(target, Stat.damage.Value);
    }

    protected override void ApplyDamage(IUnit target, float damage)
    {
        base.ApplyDamage(target, damage);
        target.TakeDamage(this, damage);
    }

    public override float TakeDamage(IUnit attacker, float damage)
    {
        base.TakeDamage(attacker, damage);

        Stat.nowHp -= damage;
        StartCoroutine(FlashRed());
        if(Stat.nowHp <= 0.01)
        {
            gameObject.SetActive(false);
        }

        return damage;
    }

    #endregion

    #region Private Methods
    private IEnumerator FlashRed()
    {
        if (rend != null)
        {
            rend.material.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            rend.material.color = originalColor;
        }
    }

    #endregion
    
}
