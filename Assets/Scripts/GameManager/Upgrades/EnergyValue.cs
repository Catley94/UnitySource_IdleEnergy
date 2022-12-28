using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergyValue : MonoBehaviour, IUpgrade
{
    [SerializeField] private GameObject upgradeEnergyValue;
    [SerializeField] private int level = 1;
    [SerializeField] private float price = 1f;

    private Purchasing purchasing;
    // Start is called before the first frame update
    void Start()
    {
        purchasing = GetComponent<Purchasing>();
        SubToEvents();
        upgradeEnergyValue.transform.Find("Level").GetComponent<TMP_Text>().text = level.ToString();
        upgradeEnergyValue.transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
        
    }

    private void SubToEvents()
    {
        purchasing.onEnergyValueUpgrade += OnPurchase;
    }

    private void OnPurchase()
    {
        level += 1;
        price += (price * 0.1f);
        upgradeEnergyValue.transform.Find("Level").GetComponent<TMP_Text>().text = level.ToString();
        upgradeEnergyValue.transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
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
