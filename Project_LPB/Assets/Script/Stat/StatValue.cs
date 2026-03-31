using UnityEngine;

[System.Serializable]
public struct StatValue
{
    [SerializeField] private float _baseValue;
    [SerializeField] private float _addValue;
    [SerializeField] private float _multiplier;
    [SerializeField] private float _finalAddValue;

    public float BaseValue { get => _baseValue; set => _baseValue = value; }
    public float AddValue { get => _addValue; set => _addValue = value; }
    public float Multiplier { get => _multiplier; set => _multiplier = value; }
    public float FinalAddValue { get => _finalAddValue; set => _finalAddValue = value; }

    /// <summary>
    /// 최종 계산된 값: (BaseValue + AddValue) * (1 + Multiplier) + FinalAddValue
    /// </summary>
    public float Value
    {
        get
        {
            return (_baseValue + _addValue) * (1f + _multiplier) + _finalAddValue;
        }
    }

    /// <summary>
    /// addValue에 값을 더합니다.
    /// </summary>
    public static StatValue operator +(StatValue stat, float value)
    {
        stat.AddValue += value;
        return stat;
    }

    /// <summary>
    /// addValue에서 값을 뺍니다.
    /// </summary>
    public static StatValue operator -(StatValue stat, float value)
    {
        stat.AddValue -= value;
        return stat;
    }

    /// <summary>
    /// multiplier에 곱연산을 적용합니다. (e.g. 20% 증가는 1.2f를 곱함). 곱연산은 중첩됩니다.
    /// </summary>
    public static StatValue operator *(StatValue stat, float factor)
    {
        // (1 + multiplier)가 실제 배율이므로, 이 값에 factor를 곱해 새로운 배율을 만듭니다.\
        stat.Multiplier += factor - 1f;
        return stat;
    }

    /// <summary>
    /// multiplier에 적용된 곱연산을 나눕니다. (e.g. 20% 증가 효과를 제거하려면 1.2f로 나눔)
    /// </summary>
    public static StatValue operator /(StatValue stat, float factor)
    {
        stat.Multiplier -= 1/factor;
        return stat;
    }

    /// <summary>
    /// 두 StatValue의 모든 구성 요소를 합산합니다.
    /// </summary>
    public static StatValue operator +(StatValue a, StatValue b)
    {
        a.BaseValue += b.BaseValue;
        a.AddValue += b.AddValue;
        a.Multiplier += b.Multiplier;
        a.FinalAddValue += b.FinalAddValue;

        return a;
    }

    /// <summary>
    /// a StatValue에서 b StatValue의 모든 구성 요소를 뺍니다.
    /// </summary>
    public static StatValue operator -(StatValue a, StatValue b)
    {
        a.BaseValue -= b.BaseValue;
        a.AddValue -= b.AddValue;
        a.Multiplier -= b.Multiplier;
        a.FinalAddValue -= b.FinalAddValue;
        return a;
    }

    /// <summary>
    /// 두 StatValue의 Multiplier를 곱연산으로 중첩 적용합니다. (e.g. 20% * 30% = 56%)
    /// </summary>
    public static StatValue operator *(StatValue a, StatValue b)
    {
        a.BaseValue *= b.BaseValue;
        a.AddValue *= b.AddValue;
        a.Multiplier *= b.Multiplier;
        a.FinalAddValue *= b.FinalAddValue;

        return a;
    }

    /// <summary>
    /// a StatValue의 Multiplier에서 b StatValue의 Multiplier를 나눗셈으로 제거합니다.
    /// </summary>
    public static StatValue operator /(StatValue a, StatValue b)
    {
        a.BaseValue /= b.BaseValue;
        a.AddValue /= b.AddValue;
        a.Multiplier /= b.Multiplier;
        a.FinalAddValue /= b.FinalAddValue;

        return a;
    }
}