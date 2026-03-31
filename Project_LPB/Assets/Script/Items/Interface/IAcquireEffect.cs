using UnityEngine;

public interface IAcquireEffect : IItemEffect
{
    void OnAcquire(IBall ball);
}
