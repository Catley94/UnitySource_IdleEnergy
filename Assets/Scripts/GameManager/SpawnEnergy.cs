using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class SpawnEnergy : MonoBehaviour
{
    [SerializeField] private GameObject energy; 
    [FormerlySerializedAs("soEnergy")] [SerializeField] private SOEnergy soEnergyBasic;
    [SerializeField] private GameObject startPosition;
    [SerializeField] private float spawnTime = 1f;
	[SerializeField] private double energypsec;
    private float second = 1f;
    public event Action<double> onUpdateEnergyPerSec;

    private void Start()
    {
        InvokeRepeating (nameof(SpawnBasic), second, spawnTime); //TODO: This will only be called once, so therefore does not get updated
		SetSpawnTime(1);
        SpawnBasic();
    }

    

    #region Utility

    public double energyPerSec()
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
        UpdateEnergyPerSec(); //TODO: Currently only updates UI
    }

    public void DecreaseSpawnTime()
    {
        spawnTime -= 0.01f;
    }

    private void UpdateEnergyPerSec()
    {
        energypsec = second / spawnTime;
        onUpdateEnergyPerSec?.Invoke(energypsec); 
    }

    #endregion
    
    #region SpawnEnergy
    
    public void SpawnBasic()
    {
        SpawnFromPool(soEnergyBasic);
    }
    
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
        GameObject _energy = Instantiate(energy, startPosition.transform.position, startPosition.transform.rotation);
        _energy.GetComponent<Health>().SetHealth(energyObject.health);

    }
    
    #endregion

    

}
