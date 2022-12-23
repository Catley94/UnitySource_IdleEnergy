using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private double money = -1;
    [SerializeField] private double moneypsec = -1;
    [SerializeField] private double energypsec = -1;
    [SerializeField] private double mainLaneEnergyPSec;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text energyText;

    [SerializeField] private GameObject tower;

    [SerializeField] private GameObject towers;

    [SerializeField] private GameObject chakra;

    [SerializeField] private SpawnEnergy mainLane;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMainLaneEnergyPSec(double _mainLaneEnergyPSec)
    {
        mainLaneEnergyPSec = _mainLaneEnergyPSec;
        energyText.text = mainLaneEnergyPSec.ToString();
        CalcMoneyPerSec();
    }
    
    private double CalcMoneyPerSec()
    {
        double energyPerMinute = mainLaneEnergyPSec * 60; //Todo: Need to swap mainLaneEnergyPSec to energypsec
        double timePerTower = energyPerMinute / tower.GetComponent<TowerHealth>().GetHealth();
        int towerCount = towers.transform.childCount;
        double timeForTowersDestroyed = timePerTower * towerCount;
        double timeForChakraAbsorbtion = chakra.GetComponent<ChakraHealth>().GetHealth() / energyPerMinute;
        double totalMinsForRound = timeForTowersDestroyed + timeForChakraAbsorbtion;
        double totalSecsForRound = timeForTowersDestroyed + timeForChakraAbsorbtion * 60;
        //When at chakra, 1<Energy Value&&EnergySpawnRate> / totalSecsForRound
        double moneyPerSec = 1 / totalSecsForRound;
        moneypsec = moneyPerSec;
        moneyText.text = moneyPerSec.ToString("F2");
        return moneyPerSec;
    }
}
