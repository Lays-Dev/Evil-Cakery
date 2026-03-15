using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    public ResourceManager resourceManager;

    public TextMeshProUGUI cakeText;
    public TextMeshProUGUI coinText;

    void Update()
    {
        if (resourceManager != null)
        {
            cakeText.text = "Cake: " + resourceManager.cakeInventory["Cake"].ToString("F0");
            coinText.text = "Money: " + resourceManager.moneyInventory["Money"].ToString("F0");
        }
    }
}