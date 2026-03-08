using UnityEngine;
using System.Collections.Generic;
using System;


#region Class and Variable Declaration
[System.Serializable]
public class CorporationLevelData
    {
        [SerializeField] private string name;
        [SerializeField] private float cost;
        [SerializeField] private float evil;
        [SerializeField] private GameObject button;
    }
#endregion


public class ResourceManager : MonoBehaviour
{
    
    public Dictionary<string,float> cakeInventory = new Dictionary<string, float>
    {
        {"Demo Cake", 0}
    }; 

    
    public Dictionary<string, float> moneyInventory = new Dictionary<string, float>
    {
        {"Demo Coin", 0}
    };

    #region Player Upgrades
    public List<CorporationLevelData> CorporationLevels = new List<CorporationLevelData>();
    #endregion
}