using System.Collections.Generic;

[System.Serializable]
public class UpgradeData
{
    public string name;
    public float cost;
    public int tier;
    public float multiplier;
    public int targetResource;
}

[System.Serializable]
public class UpgradeDataList
{
    public List<UpgradeData> upgrades;
}