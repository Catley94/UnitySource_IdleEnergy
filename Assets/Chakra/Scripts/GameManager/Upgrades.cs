using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    
    [SerializeField] private ParticleSystem rootEnergy; 
    
    [SerializeField] private float energySpawnRate = 1;
    [SerializeField] private int energySize = 2;
    [SerializeField] private int blockSize = 2;
    [SerializeField] private int tappingPower = 1;
    
    //TODO: ONLY WORKS WITH ROOT ENERGY CURRENTLY, BUT NEEDS TO WORK WITH 'CURRENT' ACTIVE ENERGY.
    //EXAMPLE: IF ON SACRAL LEVEL, LEVEL UP SACRAL ENERGY ONLY, DEPENDANT ON ROUND NUMBER
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseEnergySpawnRate()
    {
        energySpawnRate += 1f;
        ParticleSystem.EmissionModule emission = rootEnergy.emission;
        emission.rateOverTime = energySpawnRate;
    }

    public void IncreaseEnergySize()
    {
        
    }

    public void IncreaseBlockSize()
    {
        
    }

    public void IncreaseTappingPower()
    {
        
    }
}
