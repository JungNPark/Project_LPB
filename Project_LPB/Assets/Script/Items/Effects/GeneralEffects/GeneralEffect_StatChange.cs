using System;
using UnityEngine;

public class GeneralEffect_StatChange : MonoBehaviour, IGeneralEffect
{
    #region Variables

    [SerializeField]
    private int _generalID = 0;
    [SerializeField]
    private Stat _statChangeVolume;  
    [SerializeField]
    private BallStat _ballStatChangeVolume;

    #endregion
    
    #region Properties
    public int GeneralID { get => _generalID; }

    #endregion

    #region PublicMethods

    public void Execute(IUnit target)
    {
        target.Stat += _statChangeVolume;
        if(target is IBall)
        {
            IBall ball = target as IBall;
            ball.BallStat += _ballStatChangeVolume;
        }
    }

    public void Execute<T>(IUnit owner, IUnit target, T value)
    {
        Execute(owner);
    }

    public void Execute(IUnit owner, IUnit target)
    {
        Execute(owner);
    }

    #endregion
}
