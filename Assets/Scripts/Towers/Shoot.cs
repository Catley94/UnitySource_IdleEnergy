using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    [SerializeField] private GameObject spawnPoint;

    [SerializeField] private int frameCount;

    [SerializeField] private int frequencyOfProjectiles = 50; //Lower => faster
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        frameCount++;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Energy"))
        {
            if (frameCount % frequencyOfProjectiles == 0)
            {
                GameObject energy = other.gameObject;
                if (energy.GetComponent<Targeted>().GetIsTargeted()) return;
                GameObject _projectile = Instantiate(projectile, spawnPoint.transform.position, spawnPoint.transform.rotation);
                _projectile.GetComponent<Target>().SetTarget(energy);
                energy.GetComponent<Targeted>().SetTargeted(true);
                _projectile.GetComponent<Target>().SetHasTarget(energy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //destroy projectiles 
    }
}
