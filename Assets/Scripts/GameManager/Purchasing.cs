using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Purchasing : MonoBehaviour
{
    [SerializeField] private GameObject upgradeEnergyValue;
    [SerializeField] private GameObject upgradeSpawnRate;
    [SerializeField] private GameObject unlockLane;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PurchaseEnergyValue()
    {
        string price = upgradeEnergyValue.transform.Find("Price").GetComponent<TMP_Text>().text;
        string level = upgradeEnergyValue.transform.Find("Level").GetComponent<TMP_Text>().text;
        // TODO: Disable button if price > money
        // TODO: When purchasing, convert string to double / float 
        // gameObject.GetComponentInParent<CurrencyManager>().DeductMoneyByPrice(price);
    }
    
    public void PurchaseEnergySpawnRate()
    {
        
    }
    
    public void PurchaseUnlockPath()
    {
        
    }
}

