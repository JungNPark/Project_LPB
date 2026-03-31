using UnityEngine;



[System.Serializable]
public class Stat
{
    public StatValue maxHp;
    public StatValue nowHp;
    public StatValue speed;
    public StatValue size;

    public static Stat operator+(Stat a, Stat b)
    {
        if (a == null) return b;
        if (b == null) return a;
        return new Stat
        {
            maxHp = a.maxHp + b.maxHp,
            nowHp = a.nowHp + b.nowHp,
            speed = a.speed + b.speed,
            size = a.size + b.size
        };
    }

    public static Stat operator*(Stat a, Stat b)
    {
        if (a == null) return b;
        if (b == null) return a;
        return new Stat
        {
            maxHp = a.maxHp * b.maxHp,
            nowHp = a.nowHp * b.nowHp,
            speed = a.speed * b.speed,
            size = a.size * b.size
        };
    }
};
