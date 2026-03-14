// Unitys system library that has the Serializable attribute
using System;

// This class can be saved and shown in the inspector
[Serializable]
// This is the layout of each upgrade, it has a name, cost, resource type, increase amount, tier, state and effect
public class Upgrade
{
    // Name of upgrade
    public string name;
    // Cost of upgrade
    public float cost;
    // Resource that the upgrade affects
    // Example: Cake, Money, Ingredients, Bills, Evil, Market, Prestige, Auto Upgrades
    public string resourceType;
    // How much the specified resource increases when the upgrade is purchased
    public float increaseAmount;
    // Tier level of the upgrade
    public int tier;
    // The upgrade is Unpurchased, Purchased, Available, Unavailable
    public UpgradeState state;
    // What does this upgrade do
    public UpgradeEffect effect;
}