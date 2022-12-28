using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.Pool;

public class SpawnEnergy : MonoBehaviour
{
    public event Action<double> onUpdateEnergyPerSec;
    [SerializeField] private GameObject energy; 
    [FormerlySerializedAs("soEnergy")] [SerializeField] private SOEnergy soEnergyBasic;
    [SerializeField] private GameObject startPosition;
	[SerializeField] private double energypsec;
    private ChakraHealth chakra;

    private IObjectPool<GameObject> energyPool; //TODO: Make this specific to Energy
    private float second = 1f;
    private float spawnTime = 1f;
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
        chakra = gameObject.GetComponentInParent<ChakraHealth>();
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

    public void DecreaseSpawnTime()
    {
        spawnTime -= (spawnTime * 0.01f);
        // spawnTime -= 0.1f;
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
