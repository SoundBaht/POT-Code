using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{

    public static Character instance;
    public TextMeshProUGUI coinsTMP;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    [Header("References")]
    private PlayerScript playerScript;

    [Header("CurrentStatus")]
    public int currentHealth, currentCurrency, currentLevel;

    [Header("Upgrade")]
    public int PriceUpgrade;

    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        currentHealth = playerScript._hp;

        if (PlayerPrefs.HasKey("CurrentCoins")) {
            currentCurrency = PlayerPrefs.GetInt("CurrentCoins");

            currentLevel = PlayerPrefs.GetInt("LeveledUp");
            PriceUpgrade = PlayerPrefs.GetInt("PriceUpgraded");
        } else {
            currentCurrency = 0;
            Debug.Log("No Coins Data");
        }

    }

    void Update() {
        UpdateCoins();

        // if (Input.GetKeyDown("6")) {
        //     currentCurrency = 90000;
        // }
    }

    private void OnEnable()
    {
        CurrencyManager.Instance.OnCurrencyChange += HandleCurrencyChange;
    }

    private void OnDisable()
    {
        CurrencyManager.Instance.OnCurrencyChange -= HandleCurrencyChange;
    }

    public void HandleCurrencyChange(int newCurrency)
    {
        currentCurrency += newCurrency;
        
    }

    public void LevelUp()
    {
        currentCurrency = currentCurrency - PriceUpgrade;
        currentLevel++;
        PriceUpgrade = PriceUpgrade * 20 / 100 + PriceUpgrade;

        // PlayerPrefs.SetInt("UpgradedLevel", currentLevel);
        // PlayerPrefs.SetInt("UpgradedPrice", PriceUpgrade);
    }
    
    public void UpdateCoins() {
        coinsTMP.text = "Coins : " + currentCurrency;
    }
}
