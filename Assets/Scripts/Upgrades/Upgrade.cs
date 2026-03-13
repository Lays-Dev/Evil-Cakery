using System;

[Serializable]
public class Upgrade
{
    public string name;
    public float cost;
    public string resourceType;

    public float increaseAmount;

    public int tier;

    public UpgradeState state;

    public UpgradeEffect effect;
}