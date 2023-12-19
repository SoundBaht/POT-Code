using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public delegate void CurrencyChangeHandler(int amount);
    public event CurrencyChangeHandler OnCurrencyChange;

    //Singleton Check
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void addCurrency(int amount)
    {
        OnCurrencyChange?.Invoke(amount);
    }
}
