using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject chakra;
    private int trackPointIndex = 0;
    private SOEnergy soEnergy;
    [FormerlySerializedAs("speed")] [SerializeField] private float baseSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, chakra.transform.position, baseSpeed);
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
        Destroy(gameObject);
        //TODO Return to pool
    }
    

    public void SetSOEnergy(SOEnergy _soEnergy)
    {
        soEnergy = _soEnergy;
    }
}
