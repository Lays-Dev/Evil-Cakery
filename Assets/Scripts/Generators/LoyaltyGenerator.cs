using UnityEngine;

public class LoyaltyGenerator : Generator
{
    public override void Produce(ref float resourceAmount)
    {
        resourceAmount += productionRate;
    }
}
