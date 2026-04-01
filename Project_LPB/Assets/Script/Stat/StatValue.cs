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