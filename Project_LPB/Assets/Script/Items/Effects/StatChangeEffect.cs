using System;
using UnityEngine;

public class StatChangeEffect : MonoBehaviour, IAcquireEffect
{

    [SerializeField]
    private Stat _statChangeVolume;  
    [SerializeField]
    private BallStat _ballStatChangeVolume;

    public void OnAcquire(IBall ball)
    {
        ball.Stat += _statChangeVolume;
        ball.BallStat += _ballStatChangeVolume;
    }
}
