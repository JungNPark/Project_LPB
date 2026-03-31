using UnityEngine;

[System.Serializable]
public class BallStat
{
    public StatValue speed;
    public StatValue size;
    public Vector3 dir;
    public StatValue damage;

    public static BallStat operator +(BallStat a, BallStat b)
    {
        if (a == null) return b;
        if (b == null) return a;
        return new BallStat
        {
            speed = a.speed + b.speed,
            size = a.size + b.size,
            dir = a.dir + b.dir,
            damage = a.damage + b.damage
        };
    }
}
