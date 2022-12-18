using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCount : MonoBehaviour
{
    public List<GameObject> energies = new List<GameObject>();

    public event Action NO_ENERGY;
    public event Action ENERGY;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(GameObject energy)
    {  
        energies.Add(energy);
        ENERGY?.Invoke();
    }

    public void Remove(GameObject energy)
    {
        Debug.Log(energies);
        energies.Remove(energy);
        if (energies.Count == 0)
        {
            Debug.Log("No Energies on screen");
            NO_ENERGY?.Invoke();
        }
    }

}
