using UnityEngine;

public class BallBase : UnitBase, IBall
{
    #region Variables
    protected Rigidbody rb;
    [SerializeField]
    protected BallStat _ballStat;

    #endregion

    #region Properties
    public BallStat BallStat { get => _ballStat; set => _ballStat = value; }

    #endregion

    #region Unity LifeCycle
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        Vector3 dir = new Vector3(stat.dir.x, 0, stat.dir.z);
        dir = dir.normalized;
        rb.linearVelocity = dir * _stat.speed.Value;
    }

    public virtual void Shoot(Vector3 dir)
    {
        _ballStat.dir = dir;
        Shoot(_ballStat);
    }

    #endregion
}
