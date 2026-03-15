using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ResourceManager : MonoBehaviour
{

    public List<Upgrade> upgrades = new List<Upgrade>();
    
    // RESOURCE STORAGE (Dictionary requirement)
    public Dictionary<string, float> cakeInventory = new Dictionary<string, float>()
    {
        { "Cake", 0 }
    };

    public Dictionary<string, float> moneyInventory = new Dictionary<string, float>()
    {
        { "Money", 0 }
    };

    // Passive Income 
    [SerializeField] private float cakeIncomeAmount = 1f;
    [SerializeField] private float autoCakePerSec = 1f;

    [SerializeField] private float moneyIncomeAmount = 2f;
    [SerializeField] private float moneyIncomeRate = 2f;

    // UI
    [Header("UI")]
    public TextMeshProUGUI cakeText;
    public TextMeshProUGUI moneyText;

    private void Start()
    {
        StartCoroutine(CakePassiveIncome());
        StartCoroutine(MoneyPassiveIncome());
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
            cakeInventory["Cake"] += cakeIncomeAmount;

            Debug.Log("Cake: " + cakeInventory["Cake"]);

            yield return new WaitForSeconds(autoCakePerSec);
        }
    }

    private IEnumerator MoneyPassiveIncome()
    {
        while (true)
        {
            moneyInventory["Money"] += moneyIncomeAmount;

            Debug.Log("Money: " + moneyInventory["Money"]);

            yield return new WaitForSeconds(moneyIncomeRate);
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

    public void PurchaseUpgrade(int index)
    {
        if (index >= upgrades.Count) return;

        Upgrade upgrade = upgrades[index];

        if (upgrade.state == UpgradeState.Purchased)
            return;

        if (moneyInventory["Money"] >= upgrade.cost)
        {
            moneyInventory["Money"] -= upgrade.cost;

            ApplyUpgradeEffect(upgrade.effect);

            upgrade.state = UpgradeState.Purchased;

            Debug.Log("Purchased Upgrade: " + upgrade.name);
        }
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

    #endregion
}