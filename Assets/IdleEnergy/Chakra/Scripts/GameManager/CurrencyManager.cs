using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{

    [SerializeField] private int money;
    
    [SerializeField] private TMP_Text moneyText;
    
    private SceneManager sceneManager;
    
    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.FindWithTag("GameManager").GetComponent<SceneManager>();
        sceneManager.SaveBeforeLoad.AddListener(Save);
        moneyText.text = money.ToString();
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

    private void Save()
    {
        //Save money
    }

    private void Load()
    {
        
    }
}
