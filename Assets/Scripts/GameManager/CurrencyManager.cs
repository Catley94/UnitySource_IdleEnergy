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
    }
}
