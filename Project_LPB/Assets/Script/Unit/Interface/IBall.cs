using System;
using UnityEngine;

public interface IBall : IUnit
{
   event Action OnBallDead;
   BallStat BallStat { get; set; }
   void Shoot(Vector2 dir);
   void HandleBallDead();
}
