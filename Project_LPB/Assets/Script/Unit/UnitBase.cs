using UnityEngine;
using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine.PlayerLoop;


public delegate void UpdateDelegate(float deltaTime);
public delegate void TickDelegate(float tickRate);
public delegate void CollisionDelegate(Collision collision);
public delegate void AttackDelegate(IUnit attacker, IUnit target, float damage);
public delegate void HitDelegate(IUnit attacker, IUnit target, float damage);
public class UnitBase : MonoBehaviour, IUnit
{
    public UpdateDelegate updateDelegate;
    public TickDelegate tickDelegate;
    public CollisionDelegate collisionDelegate;

    public AttackDelegate attackDelegate;
    public HitDelegate hitDelegate;

    [SerializeField]
    protected Stat _stat;
    public virtual Stat Stat
    {
        get => _stat;
        set => _stat = value;
    }

    protected Dictionary<int, IBuff> _buffs = new Dictionary<int, IBuff>();

    
    private void Awake()
    {
        StartCoroutine(TickCoroutine());
    }

    private IEnumerator TickCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        while (true)
        {
            OnTick(0.1f);
            yield return wait;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate(Time.deltaTime);
    }

    protected virtual void OnUpdate(float deltaTime)
    {
        updateDelegate?.Invoke(deltaTime);
    }

    protected virtual void OnTick(float tickRate)
    {
        tickDelegate?.Invoke(tickRate);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        collisionDelegate?.Invoke(collision);
    }

    protected virtual void ApplyDamage(IUnit target, float damage)
    {
        attackDelegate?.Invoke(this, target, damage);
    }

    public virtual float TakeDamage(IUnit attacker, float damage)
    {
        hitDelegate?.Invoke(attacker, this, damage);

        return damage;
    }
    
}
