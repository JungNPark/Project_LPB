using UnityEngine;

public interface IBall : IUnit
{
   public void Shoot(Vector3 dir);
   public BallStat BallStat { get; set; }
}
