using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{

    [SerializeField] private int money;
    
    [SerializeField] private TMP_Text moneyText;
    
    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetMoney()
    {
        return money;
    }

    public void IncreaseMoneyBy(int amount)
    {
        money += amount;
        moneyText.text = money.ToString();
    }
}
