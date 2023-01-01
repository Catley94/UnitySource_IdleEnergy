using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.Pool;

public class SpawnEnergy : MonoBehaviour
{
    //Update CurrencyManager keeping count of total energyPerSec
    public event Action<double> onUpdateEnergyPerSec;
    
    //Referencing Energy Prefab so we can Instantiate
    [SerializeField] private GameObject energy; 
    //Energy Stats are stored within a Scriptable Object, however they may not be needed now
    [FormerlySerializedAs("soEnergy")] [SerializeField] private SOEnergy soEnergyBasic;
    //Energy Starting position when moving towards Chakra, different for each LaneManager
    [SerializeField] private GameObject startPosition;
    //Energy Per Second for this Lane
	[SerializeField] private double energypsec;
    //Reference of Chakra, different for each Lane
    [SerializeField] private ChakraHealth chakra;

    //For referencing
    private GameObject gameManager;
    
    //For upgrading Enerygy Value when purchased
    private EnergyValue energyValue;
    //For upgrading Energy Spawn Rate when purchased
    private EnergySpawnRate energySpawnRate;

    //Energy Pool, creation, getting, releasing, destroying
    private IObjectPool<GameObject> energyPool; //TODO: Make this specific to Energy
    //Value of 1 second
    private float second = 1f;
    //Current Spawn Time for this Lane
    private float spawnTime = 1f;
    //Coroutine depends on this to be true in order to spawn in Energies per second
    //If false, should act like pausing the game
    private bool playing = true;

    private void Awake()
    {
        energyPool = new ObjectPool<GameObject>(
                CreateEnergy,
                OnGetEnergy,
                OnReleaseEnergy,
                OnDestroyEnergy,
                maxSize: 20//TODO: Calculate how many should be on screen at any time, + 1 to be safe
            );
        
    }

    private void OnEnable()
    {
        SetupReferences();
        SubToEvents();
    }

    private void SubToEvents()
    {
        energyValue.onUpgrade += UpgradeEnergyValue;
        energySpawnRate.onUpgrade += UpgradeEnergySpawnRate;
    }

    private void UpgradeEnergyValue(float energyValue)
    {
        
    }

    private void UpgradeEnergySpawnRate()
    {
        spawnTime -= (spawnTime * 0.01f);
        UpdateEnergyPerSec();
    }

    private void SetupReferences()
    {
        gameManager = GameObject.Find("GameManager");
        energyValue = gameManager.GetComponent<EnergyValue>();
        energySpawnRate = gameManager.GetComponent<EnergySpawnRate>();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnergies()); //Starts the loop of spawning X amount of Energy per second
        SetSpawnTime(spawnTime);
    }

    private IEnumerator SpawnEnergies()
    {
        while (playing)
        {
            SpawnFromPool(soEnergyBasic);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    #region PoolingSystem

    private GameObject CreateEnergy()
    {
        return Instantiate(energy);
    }

    private void OnGetEnergy(GameObject _energy)
    {
        _energy.SetActive(true);
        _energy.transform.position = startPosition.transform.position;
        _energy.transform.rotation = startPosition.transform.rotation;
        _energy.GetComponent<Movement>().SetEnergyPool(energyPool);
        _energy.GetComponent<Movement>().SetTarget(chakra.gameObject);
    }
    
    private void OnReleaseEnergy(GameObject _energy)
    {
        _energy.SetActive(false);
    }
    
    private void OnDestroyEnergy(GameObject _energy)
    {
        Destroy(_energy);
    }

    #endregion

    #region Utility

    public double GetEnergyPerSec()
    {
        return energypsec;
    }

    public float GetSpawnTime()
    {
        return spawnTime;
    }

    public void SetSpawnTime(float _spawnTime)
    {
        spawnTime = _spawnTime;
        UpdateEnergyPerSec();
    }

    private void UpdateEnergyPerSec()
    {
        energypsec = second / spawnTime;
        onUpdateEnergyPerSec?.Invoke(energypsec); 
    }

    #endregion
    
    #region SpawnEnergy
    
    private void SpawnFromPool(SOEnergy soEnergyBasic)
    {
        switch (soEnergyBasic.type)
        {
            case "Basic":
            case "basic":
                SpawnEnergyofType(soEnergyBasic);
                break;
            default:
                break;
        }
        
    }
    
    private void SpawnEnergyofType(SOEnergy energyObject)
    {
        energyPool.Get();

    }
    
    #endregion

    

}
