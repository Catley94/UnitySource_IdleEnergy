using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }
    
    [SerializeField] private double money = 1;
    [FormerlySerializedAs("mainLaneEnergyPSec")] [SerializeField] private double totalEnergyPerSec;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text energyPSecText;
   

    [SerializeField] private GameObject tower; //TODO: Am I using this anymore?

    [SerializeField] private GameObject towers;
    
    [SerializeField] private GameObject chakra;
    //All Lane Managers Reference, using to listen for Energy Update on each lane
    [SerializeField] private SpawnEnergy laneManager0;
    [SerializeField] private SpawnEnergy laneManager1;
    [SerializeField] private SpawnEnergy laneManager2;
    [SerializeField] private SpawnEnergy laneManager3;
    [SerializeField] private SpawnEnergy laneManager4;
    [SerializeField] private SpawnEnergy laneManager5;
    [SerializeField] private SpawnEnergy laneManager6;

    private EnergyValue energyValue;
    private float energyAbsorbtionValue = 1f;
    private EnergySpawnRate energySpawnRate;
    private UnlockLane unlockLane;
    private UnlockChakra unlockChakra;
    
    //New System
    [SerializeField] private float energyValueWorth = 0.01f;
    
    //Send money update as Event so we can check if the player can purchase upgrades
    //Buttons will enable by themselves if money > price of each upgrade
    public event Action<double> onMoneyUpdate;
    
    private void OnEnable()
    {
        energyValue = GameObject.Find("GameManager").GetComponent<EnergyValue>();
        energySpawnRate = GameObject.Find("GameManager").GetComponent<EnergySpawnRate>();
        unlockLane = GameObject.Find("GameManager").GetComponent<UnlockLane>();
        unlockChakra = GameObject.Find("GameManager").GetComponent<UnlockChakra>();
        SubToEvents();
    }

    private void Awake()
    {
        // moneyText = GameObject.FindGameObjectsWithTag();
        // energyPSecText
        // towers
        // chakra
        // laneManager0
        // laneManager1
        // laneManager2
        // laneManager3
        // laneManager4
        // laneManager5
        // laneManager6
        // energyWorthValue = 2;

    }

    private void Start()
    {
        totalEnergyPerSec = 1f;
    }

    private void SubToEvents()
    {
        laneManager0.onUpdateEnergyPerSec += UpdateEnergyPSec; //TODO: Need to unsub when destroyed or onDisable
        laneManager1.onUpdateEnergyPerSec += UpdateEnergyPSec; //TODO: Need to unsub when destroyed or onDisable
        laneManager2.onUpdateEnergyPerSec += UpdateEnergyPSec; //TODO: Need to unsub when destroyed or onDisable
        laneManager3.onUpdateEnergyPerSec += UpdateEnergyPSec; //TODO: Need to unsub when destroyed or onDisable
        laneManager4.onUpdateEnergyPerSec += UpdateEnergyPSec; //TODO: Need to unsub when destroyed or onDisable
        laneManager5.onUpdateEnergyPerSec += UpdateEnergyPSec; //TODO: Need to unsub when destroyed or onDisable
        laneManager6.onUpdateEnergyPerSec += UpdateEnergyPSec; //TODO: Need to unsub when destroyed or onDisable
        energyValue.onUpgrade += UpgradeEnergyValue;
        energySpawnRate.onUpgrade += UpgradeEnergySpawnRate;
        unlockLane.onUpgrade += UpgradeUnlockLane;
        unlockChakra.onUpgrade += UpgradeUnlockChakra;
    }

    private void UpgradeEnergyValue(float _energyValue)
    {
        energyAbsorbtionValue = _energyValue;
        onMoneyUpdate?.Invoke(money);
    }

    private void UpgradeEnergySpawnRate()
    {
        onMoneyUpdate?.Invoke(money);
    }
    
    private void UpgradeUnlockLane()
    {
        onMoneyUpdate?.Invoke(money);
    }
    
    private void UpgradeUnlockChakra()
    {
        onMoneyUpdate?.Invoke(money);
    }

    public void UpdateEnergyPSec(double _energyPerSec)
    {
        SetEnergyPerSec(_energyPerSec);
    }

    private void SetEnergyPerSec(double _energyPerSec)
    {
        // totalEnergyPerSec += (_energyPerSec - totalEnergyPerSec);
        totalEnergyPerSec += _energyPerSec;
        GetComponent<EnergySpawnRate>().SetRate(totalEnergyPerSec);
        energyPSecText.text = totalEnergyPerSec.ToString("F2");
    }

    

    #region Utilities

    // private void UpdateMoneyCount()
    // {
    //     money += moneypsec;
    //     moneyText.text = money.ToString("F2");
    //     onMoneyUpdate?.Invoke(money);
    // }
    
    // private void CalcMoneyPerSec() 
    // {
    //     /*
    //      TODO: Instead of Calculating Money Per Second, just add on to money when Energy Collides with Towers and -
    //         -other objects, won't be easily about to work out money p/sec but could hide that as they will still
    //         see money increase per energy. 
    //     */
    //     
    //     if (tower == null)
    //     {
    //         tower = towers.transform.GetChild(0).GetChild(0).gameObject; //if there are no towers?
    //     }
    //     double energyPerMinute = totalEnergyPerSec * 60; //Todo: Need to swap mainLaneEnergyPSec to energypsec
    //     double timePerTower =  tower.GetComponent<TowerHealth>().GetHealth() / energyPerMinute;
    //     int towerCount = towers.transform.childCount;
    //     double timeForTowersDestroyed = timePerTower * towerCount;
    //     double timeForChakraAbsorbtion = chakra.GetComponent<ChakraHealth>().GetHealth() / energyPerMinute;
    //     double totalMinsForRound = timeForTowersDestroyed + timeForChakraAbsorbtion;
    //     double totalSecsForRound = timeForTowersDestroyed + timeForChakraAbsorbtion * 60;
    //     
    //     //When at chakra, 1<Energy Value&&EnergySpawnRate> / totalSecsForRound
    //     double moneyPerSec = (energyAbsorbtionValue * totalEnergyPerSec) / totalSecsForRound;
    //     moneypsec = moneyPerSec;
    //     moneyPSecText.text = moneyPerSec.ToString("F2");
    //     
    // }

    private void UpdateMoneyText(float _money) => UpdateMoneyText(_money);

    private void UpdateMoneyText(double _money)
    {
        moneyText.text = _money.ToString("F2");
    }

    #endregion

    #region MoneyManagement

    public double GetMoney()
    {
        return money;
    }

    public bool CanPurchase(double price)
    {
        if (money - price < 0f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    public void DeductMoneyByPrice(double price)
    {
        if (CanPurchase(price))
        {
            money -= price;
            moneyText.text = money.ToString("F2");
        }
    }

    #endregion

    #region NewMoneySystem

    public void EnergyHit()
    {
        money += energyValueWorth;
        UpdateMoneyText(money);
    }

    #endregion
    
}
