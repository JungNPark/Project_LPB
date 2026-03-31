using UnityEngine;
using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;


delegate void UpdateDelegate(float deltaTime);
delegate void TickDelegate(float tickRate);
delegate void CollisionDelegate(Collision collision);
public class UnitBase : MonoBehaviour, IUnit
{
    UpdateDelegate updateDelegate;
    TickDelegate tickDelegate;
    CollisionDelegate collisionDelegate;

    [SerializeField]
    private Stat _stat;
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
}
