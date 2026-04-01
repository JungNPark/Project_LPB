using UnityEngine;
using System.Collections;

[System.Serializable]
public struct EnemyStat
{
    public float maxHp;
    public float nowHp;
};

public class Enemy : UnitBase
{
    public EnemyStat stat;
    private Renderer rend;
    private Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stat.nowHp = stat.maxHp;
        rend = GetComponent<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override float TakeDamage(IUnit attacker, float damage)
    {
        base.TakeDamage(attacker, damage);

        stat.nowHp -= damage;
        StartCoroutine(FlashRed());
        if(stat.nowHp <= 0.01)
        {
            gameObject.SetActive(false);
        }

        return damage;
    }

    IEnumerator FlashRed()
    {
        if (rend != null)
        {
            rend.material.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            rend.material.color = originalColor;
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
