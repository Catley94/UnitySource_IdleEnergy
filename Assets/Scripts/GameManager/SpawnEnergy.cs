using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnEnergy : MonoBehaviour
{
    [SerializeField] private GameObject energy; 
    [SerializeField] private GameObject startPosition;
    private ObjectCount _objectCount;
    [FormerlySerializedAs("soEnergy")] [SerializeField] private SOEnergy soEnergyBasic;
    [FormerlySerializedAs("soEnergy")] [SerializeField] private SOEnergy soEnergyFast;
    [FormerlySerializedAs("soEnergy")] [SerializeField] private SOEnergy soEnergyFierce;
    [FormerlySerializedAs("soEnergy")] [SerializeField] private SOEnergy soEnergyDefencive;
    [SerializeField] private MoneyManagement moneyManagement;
   
    private void Start()
    {
        _objectCount = GameObject.Find("GameManager").GetComponent<ObjectCount>();
    }
    public void SpawnBasic(int trackPoint)
    {
        SpawnFromPool(trackPoint, soEnergyBasic);
    }

    private void SpawnFromPool(int trackPoint, SOEnergy energyObject)
    {
        switch (energyObject.type)
        {
            case "Basic":
            case "basic":
                SpawnEnergyofType(trackPoint, soEnergyBasic);
                break;
            default:
                break;
        }
        
    }

    private void SpawnEnergyofType(int trackPoint, SOEnergy energyObject)
    {
        if (moneyManagement.DecreaseMoney(energyObject.price)) //10 being the cost of the basic Energy
        {
            float gameSpeedMultiplier = GameObject.Find("GameManager").GetComponent<Speed>().GetGameSpeed();
            GameObject _energy = Instantiate(energy, startPosition.transform.position, startPosition.transform.rotation);
            _energy.GetComponent<Movement>().SetSOEnergy(energyObject);
            _energy.GetComponent<Movement>().SetTrack(trackPoint);
            _energy.GetComponent<Health>().SetHealth(energyObject.health);
            _objectCount.Add(_energy);
        }
        else
        {
            Debug.Log("Unable to spawn Energy, not enough money");
        }
    }
}
