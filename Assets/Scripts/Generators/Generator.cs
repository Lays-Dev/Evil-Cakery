using UnityEngine;

// abstract class for resource generators
// Prpoduce() methods
// abstract classes cannot be used directly
// Uses ref parameter

public abstract class Generator : MonoBehaviour
{
    public float productionRate = 1f;
    public ResourceType resourceType;

    // Abstract method (must be implemented by child classes)
    public abstract void Produce(ref float resourceAmount);
}