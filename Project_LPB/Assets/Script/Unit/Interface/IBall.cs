using UnityEngine;

public interface IBall : IUnit
{
   void Shoot(Vector2 dir);
   BallStat BallStat { get; set; }
}
