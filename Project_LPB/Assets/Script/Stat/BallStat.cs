using UnityEngine;

[System.Serializable]
public class BallStat
{
    public Vector3 dir;
    public StatValue criticalChance;
    public StatValue cirticalDamage;
    

    public static BallStat operator +(BallStat a, BallStat b)
    {
        if (a == null) return b;
        if (b == null) return a;
        return new BallStat
        {
            criticalChance = a.criticalChance + b.criticalChance,
            cirticalDamage = a.cirticalDamage + b.cirticalDamage
        };
    }

    public static BallStat operator *(BallStat a, BallStat b)
    {
        if (a == null) return b;
        if (b == null) return a;
        return new BallStat
        {
            criticalChance = a.criticalChance * b.criticalChance,
            cirticalDamage = a.cirticalDamage * b.cirticalDamage
        };
    }
}