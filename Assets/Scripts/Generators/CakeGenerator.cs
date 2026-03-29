using UnityEngine;

public class CakeGenerator : Generator
{
    public override void Produce(ref float resourceAmount)
    {
        resourceAmount += productionRate;
    }
}