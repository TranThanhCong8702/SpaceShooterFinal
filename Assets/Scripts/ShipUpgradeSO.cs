using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSO", menuName = "new Upgrade")]
public class ShipUpgradeSO : ScriptableObject
{
    [SerializeField] string BuffName;
    [SerializeField] float buffData;
    [SerializeField] float buffCost;
    public string GetBuffName()
    {
        return BuffName;
    }
    public float GetBuffData()
    {
        return buffData;
    }
    public float GetBuffCost()
    {
        return buffCost;
    }
}
