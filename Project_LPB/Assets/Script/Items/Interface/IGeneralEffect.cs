using System.Buffers;
using UnityEngine;

public interface IGeneralEffect
{
    public int GeneralID { get; }
    public void Execute<T>(IUnit owner, IUnit other, T value)
    {
        Debug.Log("IGeneralEffect - Call Interface Method");
    }
    public void Execute(IUnit owner, IUnit other)
    {
        Debug.Log("IGeneralEffect - Call Interface Method");
    }
    public void Execute(IUnit owner)
    {
        Debug.Log("IGeneralEffect - Call Interface Method");
    }
}