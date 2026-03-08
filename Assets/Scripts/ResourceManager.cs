using UnityEngine;
using System.Collections.Generic;
using System;


#region Class and Variable Declaration
[System.Serializable]
public class UpgradeData
    {
        public string upgradeName;
        public float cost;
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
    public List<UpgradeData> playerUpgrades = new List<UpgradeData>();
    #endregion
}