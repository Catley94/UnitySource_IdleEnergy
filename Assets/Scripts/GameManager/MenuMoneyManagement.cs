using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuMoneyManagement : MonoBehaviour
{
    [SerializeField] private int money;

    [SerializeField] private TMP_Text moneyText;

    public event Action NO_MONEY;
    // Start is called before the first frame update
    void Start()
    {
        money = 200;
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

    private void SetMoney(int amount)
    {
        money = amount;
        moneyText.text = money.ToString();
        if (money == 0)
        {
            Debug.Log("Money is 0");
            NO_MONEY?.Invoke();
        }
    }

    public bool DecreaseMoney(int amount)
    {
        if (money - amount < 0) return false;
        else
        {
            SetMoney(money -= amount);
            return true;
        }

        
    }
    
    public void IncreaseMoney(int amount)
    {
        SetMoney(money += amount);
    }
}
