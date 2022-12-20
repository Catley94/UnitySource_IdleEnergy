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
    
    private void CalcMoneyPerSec()
    {
        double energyPerMinute = energypsec * 60;
        double timePerTower = energyPerMinute / tower.GetComponent<TowerHealth>().GetHealth();
        int towerCount = towers.transform.childCount;
        // double timeForTowersDestroyed = timePerTower * 
    }
}
