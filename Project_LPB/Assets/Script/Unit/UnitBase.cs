using UnityEngine;
using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine.PlayerLoop;

#region Declaring Delegates
//Update가 호출될 때마다 작동하는 델리게이트
public delegate void UpdateDelegate(float deltaTime);
//별도로 추가한 Tick 함수가 호출될 때마다 작동하는 델리게이트
public delegate void TickDelegate(float tickRate);
//CollisionEnter가 호출될 때마다 작동하는 델리게이트
public delegate void CollisionDelegate(Collision collision);
//ApplyDamage()가 호출될 때마다 작동하는 델리게이트
public delegate void AttackDelegate(IUnit attacker, IUnit target, float damage);
//TakeDamage()가 호출될 때마다 작동하는 델리게이트
public delegate void HitDelegate(IUnit attacker, IUnit target, float damage);
#endregion
public class UnitBase : MonoBehaviour, IUnit
{

    #region delegate Fields
    public UpdateDelegate updateDelegate;
    public TickDelegate tickDelegate;
    public CollisionDelegate collisionDelegate;
    public AttackDelegate attackDelegate;
    public HitDelegate hitDelegate;
#endregion

    #region Variables
    
    [SerializeField]
    protected Stat _stat;

    #endregion

    #region Properties
    public virtual Stat Stat
    {
        get => _stat;
        set => _stat = value;
    }
    protected Dictionary<int, IBuff> _buffs = new Dictionary<int, IBuff>();

#endregion

    #region Unity LifeCycle
    private void Awake()
    {
        StartCoroutine(TickCoroutine());
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
#endregion

    #region Private Methods
    /// <summary>
    /// 정해진 시간마다 Tick 함수를 호출
    /// </summary>
    /// <returns></returns>
    private IEnumerator TickCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f); //TODO: 틱 시간을 조절할 수 있는 공간 만들기
        while (true)
        {
            OnTick(0.1f); //TODO: 틱 시간 변수로 변경
            yield return wait;
        }
    }
#endregion

    #region Invoke Delegates
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
#endregion
    
}
