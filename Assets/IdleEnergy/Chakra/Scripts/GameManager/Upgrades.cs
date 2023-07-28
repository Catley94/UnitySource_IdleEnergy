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
    
    [SerializeField] private float increaseSpawnRateBy = 1f;
    [SerializeField] private float increaseEnergySizeBy = 1f;
    [SerializeField] private float increaseBlockSizeBy = 0.1f;
    [SerializeField] private float increaseTappingPowerBy = 1f;
    
    private SceneManager sceneManager;
    
    private GameObject blocksContainer;
    
    //TODO: ONLY WORKS WITH ROOT ENERGY CURRENTLY, BUT NEEDS TO WORK WITH 'CURRENT' ACTIVE ENERGY.
    //EXAMPLE: IF ON SACRAL LEVEL, LEVEL UP SACRAL ENERGY ONLY, DEPENDANT ON ROUND NUMBER
    
    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.FindWithTag("GameManager").GetComponent<SceneManager>();
        sceneManager.SaveBeforeLoad.AddListener(Save);
        blocksContainer = GameObject.FindWithTag("Blocks");
    }

    public void IncreaseEnergySpawnRate()
    {
        energySpawnRate += increaseSpawnRateBy;
        ParticleSystem.EmissionModule emission = rootEnergy.emission;
        emission.rateOverTime = energySpawnRate;
    }

    public void IncreaseEnergySize()
    {
        energySize += increaseEnergySizeBy;
        ParticleSystem.MainModule main = rootEnergy.main;
        main.startSize = energySize;
    }

    public void IncreaseBlockSize()
    {
        blockSize += increaseBlockSizeBy;
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
        tappingPower += increaseTappingPowerBy;
        GameObject.FindWithTag("GameManager").GetComponent<TappingManager>().SetTappingPower(tappingPower);
    }
    
    private void Save()
    {
        //TODO DO I need to save anything here? Probably as we're going on to Meridian scene
    }

    private void Load()
    {
        
    }
}
