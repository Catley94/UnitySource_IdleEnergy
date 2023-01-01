using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    private IObjectPool<GameObject> energyPool; //TODO: Make this specific to Energy
    private GameObject chakra;
    private SOEnergy soEnergy;
    [FormerlySerializedAs("speed")] [SerializeField] private float baseSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (chakra)
        {
            transform.position = Vector3.MoveTowards(transform.position, chakra.transform.position, baseSpeed);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chakra"))
        {
            Debug.Log("Energy absorbed");
            ReturnToPool();
        } else if (other.gameObject.CompareTag("Portal"))
        {
            Debug.Log("Energy absorbed");
            ReturnToPool();
        } else if (other.gameObject.CompareTag("Tower"))
        {
            Debug.Log("Energy Destroyed");
            other.gameObject.GetComponent<TowerHealth>().TakeDamage();
            ReturnToPool();
        }
    }
    
    private void ReturnToPool()
    {
        Debug.Log("Returned to Pool");
        energyPool.Release(this.gameObject);
    }

    public void SetEnergyPool(IObjectPool<GameObject> pool)
    {
        energyPool = pool;
    }


    public void SetSOEnergy(SOEnergy _soEnergy)
    {
        soEnergy = _soEnergy;
    }

    public void SetTarget(GameObject target)
    {
        chakra = target;
    }
}
