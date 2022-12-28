using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private double money = 1;
    [SerializeField] private double moneypsec = 1;
    [SerializeField] private double energypsec = 1;
    [SerializeField] private double mainLaneEnergyPSec;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text moneyPSecText;
    [SerializeField] private TMP_Text energyPSecText;

    [SerializeField] private GameObject tower;

    [SerializeField] private GameObject towers;

    [SerializeField] private GameObject chakra;

    [FormerlySerializedAs("mainLane")] [SerializeField] private SpawnEnergy laneManager;
    
    private void OnEnable()
    {
        SubToEvents();
        InvokeRepeating(nameof(UpdateMoneyCount), 0f, 1f);
    }

    private void SubToEvents()
    {
        laneManager.onUpdateEnergyPerSec += SetMainLaneManagerEnergyPSec; //TODO: Need to unsub when destroyed or onDisable
    }

    private void SetMainLaneManagerEnergyPSec(double _mainLaneEnergyPSec)
    {
        mainLaneEnergyPSec = _mainLaneEnergyPSec;
        energyPSecText.text = mainLaneEnergyPSec.ToString("F2");
        CalcMoneyPerSec();
    }

    #region Utilities

    private void UpdateMoneyCount()
    {
        money += moneypsec;
        moneyText.text = money.ToString("F2");
    }
    
    private void CalcMoneyPerSec()
    {
        tower = towers.transform.GetChild(0).GetChild(0).gameObject;
        double energyPerMinute = mainLaneEnergyPSec * 60; //Todo: Need to swap mainLaneEnergyPSec to energypsec
        double timePerTower =  tower.GetComponent<TowerHealth>().GetHealth() / energyPerMinute;
        int towerCount = towers.transform.childCount;
        double timeForTowersDestroyed = timePerTower * towerCount;
        double timeForChakraAbsorbtion = chakra.GetComponent<ChakraHealth>().GetHealth() / energyPerMinute;
        double totalMinsForRound = timeForTowersDestroyed + timeForChakraAbsorbtion;
        double totalSecsForRound = timeForTowersDestroyed + timeForChakraAbsorbtion * 60;
        //When at chakra, 1<Energy Value&&EnergySpawnRate> / totalSecsForRound
        double moneyPerSec = 1 / totalSecsForRound;
        moneypsec = moneyPerSec;
        moneyPSecText.text = moneyPerSec.ToString("F2");
    }

    #endregion

    #region MoneyManagement

    public double GetMoney()
    {
        return money;
    }

    public bool DeductMoneyByPrice(double price)
    {
        if (money - price < 0f)
        {
            return false;
        }
        else
        {
            money -= price;
            return true;
        }
    }

    #endregion

    
}
