using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    [SerializeField] private int health = -1;
    [SerializeField] private SOTower towerConfig;    
    
    private void OnEnable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SOTower GetTowerConfig()
    {
        return towerConfig;
    }

    public void SetTowerConfig(SOTower _towerConfig)
    {
        towerConfig = _towerConfig;
        SetHealth();
        
    }

    private void SetHealth()
    {
        health = towerConfig.baseTowerHealth; //Times round number   
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        Debug.Log("Tower Destroyed");
        Destroy(gameObject); //TODO Need to return  back to pool
    }

    public int GetHealth()
    {
        return health;
    }
}
