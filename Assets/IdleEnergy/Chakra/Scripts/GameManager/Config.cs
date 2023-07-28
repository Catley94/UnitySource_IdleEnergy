using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{

    [SerializeField] private SORoundConfig config;
    
    public SORoundConfig GetConfig()
    {
        return config;
    }
}
