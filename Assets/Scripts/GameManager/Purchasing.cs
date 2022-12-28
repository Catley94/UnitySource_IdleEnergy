using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Purchasing : MonoBehaviour
{
    
    private EnergyValue _energyValue;
    private EnergySpawnRate _energySpawnRate;

    public event Action onEnergyValueUpgrade;
    public event Action onEnergySpawnRateUpgrade;
    public event Action onLaneUnlock;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PurchaseEnergyValue()
    {
        onEnergyValueUpgrade?.Invoke();
        
        // TODO: Disable button if price > money
        // TODO: When purchasing, convert string to double / float 
        // gameObject.GetComponentInParent<CurrencyManager>().DeductMoneyByPrice(price);
    }
    
    public void PurchaseEnergySpawnRate()
    {
        onEnergySpawnRateUpgrade?.Invoke();
    }
    
    public void PurchaseUnlockPath()
    {
        onLaneUnlock?.Invoke();
    }
}

