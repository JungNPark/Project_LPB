using UnityEngine;

[System.Serializable]
public class Stat
{
    public float maxHp;
    public float nowHp;
    public float Speed;
    public float Size;

    public static Stat operator +(Stat a, Stat b)
    {
        if (a == null) return b;
        if (b == null) return a;
        return new Stat
        {
            maxHp = a.maxHp + b.maxHp,
            nowHp = a.nowHp + b.nowHp,
            Speed = a.Speed + b.Speed,
            Size = a.Size + b.Size
        };
    }
};
