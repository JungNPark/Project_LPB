using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] testItems;
    public BallBase testBall;

    void Start()
    {
        foreach(Item item in testItems)
        {
            AcquireItem(item, testBall);
        }
    }

    public void AcquireItem(Item item, IBall ball)
    {
        item.InitItemEffects();
        foreach(var acquireEffect in item.AcquireEffects)
        {
            acquireEffect.OnAcquire(ball);
            Debug.Log("Check");
        }
    }
}
