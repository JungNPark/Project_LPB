using UnityEngine;

public class BallBase : UnitBase, IBall
{
    #region Variables
    protected Rigidbody2D rb;
    [SerializeField]
    protected BallStat _ballStat;

    #endregion

    #region Properties
    public BallStat BallStat { get => _ballStat; set => _ballStat = value; }

    #endregion

    #region Unity LifeCycle
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    
    void LateUpdate()
    {
        ApplyStat();
    }

    #endregion

    #region Private Methods

    private void ApplyStat()
    {
        rb.linearVelocity = rb.linearVelocity.normalized * _stat.speed.Value;
        transform.localScale = Vector3.one * _stat.size.Value;
    }

    #endregion

    #region Public Methods
    public virtual void Shoot(BallStat stat)
    {
        stat.dir = stat.dir.normalized;
        rb.linearVelocity = stat.dir * _stat.speed.Value;
    }

    public virtual void Shoot(Vector2 dir)
    {
        _ballStat.dir = dir;
        Shoot(_ballStat);
    }

    #endregion
}
