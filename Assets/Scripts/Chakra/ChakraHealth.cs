using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakraHealth : MonoBehaviour
{
    [SerializeField] private int health = -1;

    [SerializeField] private GameObject towers;
    // Start is called before the first frame update
    void Start()
    {
        SetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetHealth()
    {
        int towerCount = towers.transform.childCount;
        health = towers.GetComponentInChildren<TowerHealth>().GetHealth() * towerCount;
    }

    public int GetHealth()
    {
        return health;
    }
}
