using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnEnergy : MonoBehaviour
{
    [SerializeField] private GameObject energy; 
    [FormerlySerializedAs("soEnergy")] [SerializeField] private SOEnergy soEnergyBasic;
    [SerializeField] private GameObject startPosition;
    [SerializeField] private float spawnTime = 1f;

    private void Start()
    {
        InvokeRepeating (nameof(SpawnBasic), 1f, 1f);
        SpawnBasic();
    }

    private void Update()
    {
        
    }

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

}
