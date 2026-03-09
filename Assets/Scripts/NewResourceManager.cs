using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

#region Class and Variable Declaration
[System.Serializable]
public class CorporationLevelData
{
    [SerializeField] public string name;
    [SerializeField] public float cost;
    [SerializeField] public float evil;

    [SerializeField] public GameObject button;
    [SerializeField] public int paletteIndex;

    [SerializeField] public string resourceType;
    [SerializeField] public float incomeIncrease;
}
#endregion

public class NewResourceManager : MonoBehaviour
{
    // RESOURCE STORAGE (Dictionary requirement)
    public Dictionary<string, float> cakeInventory = new Dictionary<string, float>()
    {
        { "Cake", 0 }
    };

    public Dictionary<string, float> moneyInventory = new Dictionary<string, float>()
    {
        { "Coin", 0 }
    };

    // Passive Income 
    [SerializeField] private float cakeIncomeAmount = 1f;
    [SerializeField] private float cakeIncomeRate = 1f;

    [SerializeField] private float moneyIncomeAmount = 2f;
    [SerializeField] private float moneyIncomeRate = 2f;

    #region Player Upgrades
    public List<CorporationLevelData> CorporationLevels = new List<CorporationLevelData>();
    #endregion

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

    private IEnumerator CakePassiveIncome()
    {
        while (true)
        {
            cakeInventory["Cake"] += cakeIncomeAmount;

            Debug.Log("Cake: " + cakeInventory["Cake"]);

            yield return new WaitForSeconds(cakeIncomeRate);
        }
    }

    private IEnumerator MoneyPassiveIncome()
    {
        while (true)
        {
            moneyInventory["Coin"] += moneyIncomeAmount;

            Debug.Log("Money: " + moneyInventory["Coin"]);

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
        moneyInventory["Coin"] += 5;

        Debug.Log("Clicked Coin: " + moneyInventory["Coin"]);
    }

    #endregion

    #region Upgrade System

    public void BuyUpgrade(int index)
    {
        if (index >= CorporationLevels.Count)
            return;

        CorporationLevelData upgrade = CorporationLevels[index];

        if (moneyInventory["Coin"] >= upgrade.cost)
        {
            moneyInventory["Coin"] -= upgrade.cost;

            if (upgrade.resourceType == "Cake")
            {
                cakeIncomeAmount += upgrade.incomeIncrease;
            }

            if (upgrade.resourceType == "Coin")
            {
                moneyIncomeAmount += upgrade.incomeIncrease;
            }

            Debug.Log("Upgrade Purchased: " + upgrade.name);
        }
    }

    #endregion

    #region UI

    private void UpdateUI()
    {
        if (cakeText != null)
            cakeText.text = "Cake: " + cakeInventory["Cake"].ToString("F0");

        if (moneyText != null)
            moneyText.text = "Coin: " + moneyInventory["Coin"].ToString("F0");
    }

    #endregion
}