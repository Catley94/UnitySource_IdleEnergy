using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{

    
    [SerializeField] private ParticleSystem rootEnergy; 
    
    [SerializeField] private float energySpawnRate = 1f;
    [SerializeField] private float energySize = 2f;
    [SerializeField] private float blockSize = 2f;
    [SerializeField] private float tappingPower = 1f;
    
    private GameObject blocksContainer;
    
    //TODO: ONLY WORKS WITH ROOT ENERGY CURRENTLY, BUT NEEDS TO WORK WITH 'CURRENT' ACTIVE ENERGY.
    //EXAMPLE: IF ON SACRAL LEVEL, LEVEL UP SACRAL ENERGY ONLY, DEPENDANT ON ROUND NUMBER
    
    // Start is called before the first frame update
    void Start()
    {
        blocksContainer = GameObject.FindWithTag("Blocks");
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
        energySize += 1f;
        ParticleSystem.MainModule main = rootEnergy.main;
        main.startSize = energySize;
    }

    public void IncreaseBlockSize()
    {
        blockSize += 0.1f;
        Transform[] chakraBlockContainers = blocksContainer.transform.GetChild(0).GetComponentsInChildren<Transform>();
        foreach (Transform chakraBlockContainer in chakraBlockContainers)
        {
            foreach (Transform block in chakraBlockContainer)
            {
                block.localScale = new Vector3(blockSize, blockSize, blockSize);
            }
        }
    }

    public void IncreaseTappingPower()
    {
        
    }
}
