using System;
using System.Collections;
using System.Collections.Generic;
using log4net.Core;
using TMPro;
using UnityEngine;

public class EnergySpawnRate : MonoBehaviour, IUpgrade
{
    [SerializeField] private GameObject upgradeSpawnRate;
    [SerializeField] private int level = 1;
    [SerializeField] private float price = 1f;
    [SerializeField] private SpawnEnergy _spawnEnergy;
    private Purchasing purchasing;

    public event Action onUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        purchasing = GetComponent<Purchasing>();
        SubToEvents();
        upgradeSpawnRate.transform.Find("Level").GetComponent<TMP_Text>().text = level.ToString();
        upgradeSpawnRate.transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
    }

    private void SubToEvents()
    {
        purchasing.onEnergySpawnRateUpgrade += OnPurchase;
    }

    public void OnPurchase()
    {
        level += 1;
        price += (price * 0.1f);
        upgradeSpawnRate.transform.Find("Level").GetComponent<TMP_Text>().text = level.ToString();
        upgradeSpawnRate.transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
        onUpgrade?.Invoke();
        // _spawnEnergy.DecreaseSpawnTime();
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
