using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChakraHealth : MonoBehaviour
{
    [SerializeField] private int health = -1;

    [SerializeField] private GameObject towers;

    [SerializeField] private TMP_Text lifeText;
    
    // Start is called before the first frame update
    void Start()
    {
        SetHealth();
    }
    
    private void DecreaseHealth()
    {
        health -= 1;
        lifeText.text = health.ToString();
        if (health == 0)
        {
            GameObject.Find("GameManager").GetComponent<RoundManager>().NextRound();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Energy"))
        {
            DecreaseHealth();
        }
    }

    private void ResetChakraHealth()
    {
        SetHealth();
    }

    private void SetHealth()
    {
        
        if (towers)
        {
            int towerCount = towers.transform.childCount;
            
            if (towerCount > 0)
            {
                health = towers.GetComponentInChildren<TowerHealth>().GetHealth() * towerCount;
            }
            else
            {
                health = 0;
            }
            lifeText.text = health.ToString();
        }

    }

    public int GetHealth()
    {
        return health;
    }
}
