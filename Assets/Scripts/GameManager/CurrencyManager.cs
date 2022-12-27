using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private double money = -1;
    [SerializeField] private double moneypsec = -1;
    [SerializeField] private double energypsec = -1;
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
    }
    // Start is called before the first frame update
    void Start()
    {
        // SubToEvents();
    }
    
    private void SubToEvents()
    {
        laneManager.onUpdateEnergyPerSec += SetMainLaneManagerEnergyPSec; //TODO: Need to unsub when destroyed or onDisable
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMainLaneManagerEnergyPSec(double _mainLaneEnergyPSec)
    {
        mainLaneEnergyPSec = _mainLaneEnergyPSec;
        energyPSecText.text = mainLaneEnergyPSec.ToString("F2");
        CalcMoneyPerSec();
    }
    
    private void CalcMoneyPerSec()
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
        moneyPSecText.text = moneyPerSec.ToString("F2");
    }
}
