using UnityEngine;



[System.Serializable]
public class Stat
{
    public StatValue maxHp;
    public StatValue nowHp;
    public StatValue damageReduction;
    public StatValue speed;
    public StatValue size;
    public StatValue damage;
    public StatValue trueDamage;

    public static Stat operator+(Stat a, Stat b)
    {
        if (a == null) return b;
        if (b == null) return a;
        return new Stat
        {
            maxHp = a.maxHp + b.maxHp,
            nowHp = a.nowHp + b.nowHp,
            speed = a.speed + b.speed,
            size = a.size + b.size,
            damage = a.damage + b.damage,
            damageReduction = a.damageReduction + b.damageReduction,
            trueDamage = a.trueDamage + b.trueDamage
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
            size = a.size * b.size,
            damage = a.damage * b.damage,
            damageReduction = a.damageReduction * b.damageReduction,
            trueDamage = a.trueDamage * b.trueDamage
        };
    }
};
