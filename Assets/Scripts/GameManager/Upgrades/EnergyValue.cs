using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyValue : MonoBehaviour, IUpgrade
{
    [SerializeField] private GameObject upgradeEnergyValue;
    [SerializeField] private GameObject upgradeEnergyValueButton;
    [SerializeField] private int level = 1;
    [SerializeField] private float price = 1f;

    private Purchasing purchasing;
    private CurrencyManager currencyManager;
    
    public event Action onUpgrade;
    
    // Start is called before the first frame update
    void Start()
    {
        purchasing = GetComponent<Purchasing>();
        currencyManager = GetComponent<CurrencyManager>();
        SubToEvents();
        upgradeEnergyValue.transform.Find("Level").GetComponent<TMP_Text>().text = level.ToString();
        upgradeEnergyValue.transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
        
    }

    private void SubToEvents()
    {
        purchasing.onEnergyValueUpgrade += OnPurchase;
        currencyManager.onMoneyUpdate += OnMoneyUpdate;
    }

    private void OnPurchase()
    {
        //TODO: Purposely commented out the below as need to implement this feature
        // level += 1;
        // price += (price * 0.1f);
        currencyManager.DeductMoneyByPrice(price);
        upgradeEnergyValue.transform.Find("Level").GetComponent<TMP_Text>().text = level.ToString();
        upgradeEnergyValue.transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
        onUpgrade?.Invoke();
    }
    
    private void ResetValues()
    {
        level = 1;
        price = 1;
        upgradeEnergyValue.transform.Find("Level").GetComponent<TMP_Text>().text = level.ToString();
        upgradeEnergyValue.transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
    }
    
    private void OnMoneyUpdate(double money)
    {
        if (currencyManager.CanPurchase(price))
        {
            //TODO: Enable Unlock Lane button (if disabled)
            upgradeEnergyValueButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            //TODO: Disable button (if enabled)
            upgradeEnergyValueButton.GetComponent<Button>().interactable = false;
        }
    }


    public int GetLevel()
    {
        return level;
    }

    public float GetPrice()
    {
        return price;
    }
}
