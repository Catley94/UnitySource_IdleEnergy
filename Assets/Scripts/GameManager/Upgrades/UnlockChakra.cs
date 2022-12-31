using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnlockChakra : MonoBehaviour, IUpgrade
{
    //Price of unlocking Chakra
    [SerializeField] private float price = 10f;
    
    //For Referencing
    
    [SerializeField] private GameObject unlockChakra;
    //For Enabling and Disabling
    [SerializeField] private GameObject chakraLock;
    //For Enabling and Disabling
    [SerializeField] private GameObject portal;

    private Purchasing purchasing;
    private CurrencyManager currencyManager;
    
    // Start is called before the first frame update
    void Start()
    {
        purchasing = GetComponent<Purchasing>();
        currencyManager = GetComponent<CurrencyManager>();
        SubToEvents();
        unlockChakra.transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
    }

    private void SubToEvents()
    {
        purchasing.onChakraUnlock += OnPurchase;
    }

    private void OnPurchase()
    {
        if (currencyManager.CanPurchase(price))
        {
            currencyManager.DeductMoneyByPrice(price);
            chakraLock.SetActive(false);
            portal.SetActive(false);
            unlockChakra.SetActive(false);
        }
    }

    private void ResetValues()
    {
        chakraLock.SetActive(true);
        portal.SetActive(true);
        unlockChakra.SetActive(true);
    }

    public int GetLevel() => 0;

    public float GetPrice() => price;
}
