using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public List<Generator> generators = new List<Generator>();

    public List<Upgrade> upgrades = new List<Upgrade>();

    #region Dictionary
    
    // RESOURCE STORAGE (Dictionary requirement)
    public Dictionary<string, float> cakeInventory = new Dictionary<string, float>()
    {
        { "Cake", 0 }
    };

    public Dictionary<string, float> moneyInventory = new Dictionary<string, float>()
    {
        { "Money", 0 },
        { "Loyalty", 0 }
    };

    #endregion

    // Passive Income 
    [SerializeField] private float cakeIncomeAmount = 1f;
    [SerializeField] private float autoCakePerSec = 1f;

    [SerializeField] private float moneyIncomeAmount = 2f;
    [SerializeField] private float moneyIncomeRate = 2f;
    private bool loyaltyUnlocked = false;

    // UI
    [Header("UI")]
    public TextMeshProUGUI cakeText;
    public TextMeshProUGUI moneyText;

    private void Start()
    {
        StartCoroutine(CakePassiveIncome());
        StartCoroutine(SellCakes());
        StartCoroutine(LoyaltyPassiveIncome());
    }

    private void Update()
    {
        UpdateUI();
    }

    #region Passive Income

    // Logic to apply purchased upgrades to income rate ( Assignment Lab9)
    private IEnumerator CakePassiveIncome()
    {
        while (true)
        {
            float cake = cakeInventory["Cake"];

            foreach (Generator gen in generators)
            {
                if (gen is CakeGenerator)
                {
                    gen.Produce(ref cake);
                }
            }

            cakeInventory["Cake"] = cake;

            yield return new WaitForSeconds(autoCakePerSec);
        }
    }

    // Cake selling logic 
    private IEnumerator SellCakes()
    {
        while (true)
        {
            float cakes = cakeInventory["Cake"];

            if (cakes > 0)
            {

                // Loyalty makes selling faster
                float sellAmount = 1f + moneyInventory["Loyalty"];

                // Clamp so we don't sell more than we have
                sellAmount = Mathf.Min(sellAmount, cakes);

                cakeInventory["Cake"] -= sellAmount;

                // Convert to money
                moneyInventory["Money"] += sellAmount * 2f;

                Debug.Log("Sold Cakes: " + sellAmount);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator LoyaltyPassiveIncome()
    {
        while (true)
        {
            if (!loyaltyUnlocked)
            {
                yield return null;
                continue;
            }

            float loyalty = moneyInventory["Loyalty"];

            foreach (Generator gen in generators)
            {
                if (gen is LoyaltyGenerator)
                {
                    gen.Produce(ref loyalty);
                }
            }

            moneyInventory["Loyalty"] = loyalty;

            yield return new WaitForSeconds(1f);
        }
    }

    #endregion

    #region Manual Click

    public void ClickCake()
    {
        cakeInventory["Cake"] += 5;

        Debug.Log("Clicked Cake: " + cakeInventory["Cake"]);
    }

    public void ClickMoney()
    {
        moneyInventory["Money"] += 5;

        Debug.Log("Clicked Money: " + moneyInventory["Money"]);
    }

    #endregion

    #region Upgrade System

    public void PurchaseUpgrade(int index, out bool success, out string message)
    {
        success = false;
        message = "";

        if (index >= upgrades.Count)
        {
            message = "Invalid upgrade index";
            return;
        }

        Upgrade upgrade = upgrades[index];

        if (upgrade.state == UpgradeState.Purchased)
        {
            message = "Already purchased";
            return;
        }

        if (moneyInventory["Money"] < upgrade.cost)
        {
            message = "Not enough money";
            return;
        }

        moneyInventory["Money"] -= upgrade.cost;

        ApplyUpgradeEffect(upgrade.effect);

        upgrade.state = UpgradeState.Purchased;

        success = true;
        message = "Upgrade purchased: " + upgrade.name;

        Debug.Log(message);
    }

    void ApplyUpgradeEffect(UpgradeEffect effect)
    {
        if (effect.targetResource == ResourceType.Cake)
        {
            cakeIncomeAmount *= effect.multiplier;
        }

        if (effect.targetResource == ResourceType.Money)
        {
            moneyIncomeAmount *= effect.multiplier;
        }

        if (effect.targetResource == ResourceType.Loyalty)
        {
            loyaltyUnlocked = true;
        }
    }

    #endregion

    #region UI

    private void UpdateUI()
    {
        if (cakeText != null)
            cakeText.text = "Cake: " + cakeInventory["Cake"].ToString("F0");

        if (moneyText != null)
            moneyText.text = "Money: " + moneyInventory["Money"].ToString("F0");
    }

    public void PurchaseUpgradeButton(int index)
    {
        bool success;
        string message;

        PurchaseUpgrade(index, out success, out message);

        Debug.Log(message);
        Debug.Log("Button Pressed: " + index);
    }

    #endregion
}