using UnityEngine;

public interface IBall : IUnit
{
   void Shoot(Vector3 dir);
   BallStat BallStat { get; set; }
}
