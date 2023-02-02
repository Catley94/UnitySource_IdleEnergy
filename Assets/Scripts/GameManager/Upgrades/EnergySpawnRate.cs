using System;
using System.Collections;
using System.Collections.Generic;
using log4net.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergySpawnRate : MonoBehaviour, IUpgrade
{
    [SerializeField] private GameObject upgradeSpawnRate;
    [SerializeField] private GameObject upgradeSpawnRateButton;
    [SerializeField] private int level = 1;
    [SerializeField] private float price = 1f;
    [SerializeField] private SpawnEnergy _spawnEnergy;
    [SerializeField] private double rate = 1f;
    [SerializeField] private double time = 1f;
    private Purchasing purchasing;
    private CurrencyManager currencyManager;

    public event Action onUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        purchasing = GetComponent<Purchasing>();
        currencyManager = GetComponent<CurrencyManager>();
        SubToEvents();
        upgradeSpawnRate.transform.Find("Level").GetComponent<TMP_Text>().text = level.ToString();
        upgradeSpawnRate.transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
    }

    private void SubToEvents()
    {
        purchasing.onEnergySpawnRateUpgrade += OnPurchase;
        // currencyManager.onMoneyUpdate += OnMoneyUpdate;
    }

    public void OnPurchase()
    {
        currencyManager.DeductMoneyByPrice(price);
        level += 1;
        price += (price * 0.1f);
        upgradeSpawnRate.transform.Find("Level").GetComponent<TMP_Text>().text = level.ToString();
        upgradeSpawnRate.transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
        onUpgrade?.Invoke();
        // _spawnEnergy.DecreaseSpawnTime();
    }

    public double GetRate()
    {
        return rate;
    }

    public void SetRate(double _rate)
    {
        rate = _rate;
    }

    public double GetSpawnTime()
    {
        return time;
    }

    public void SetSpawnTime(double _time)
    {
        time = _time;
    }

    // private void OnMoneyUpdate(double money)
    // {
    //     if (currencyManager.CanPurchase(price))
    //     {
    //         //TODO: Enable Unlock Lane button (if disabled)
    //         upgradeSpawnRateButton.GetComponent<Button>().interactable = true;
    //     }
    //     else
    //     {
    //         //TODO: Disable button (if enabled)
    //         upgradeSpawnRateButton.GetComponent<Button>().interactable = false;
    //     }
    // }

    private void Update()
    {
        if (currencyManager.CanPurchase(price))
        {
            //TODO: Enable Unlock Lane button (if disabled)
            upgradeSpawnRateButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            //TODO: Disable button (if enabled)
            upgradeSpawnRateButton.GetComponent<Button>().interactable = false;
        }
    }

    private void ResetValues()
    {
        level = 1;
        price = 1;
        upgradeSpawnRate.transform.Find("Level").GetComponent<TMP_Text>().text = level.ToString();
        upgradeSpawnRate.transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
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
