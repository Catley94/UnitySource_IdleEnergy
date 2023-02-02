using System;
using System.Collections;
using System.Collections.Generic;
using PlasticGui.WebApi.Responses;
using UnityEngine;

public class EnergyHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (
            other.CompareTag("Tower") ||
            other.CompareTag("Portal") ||
            other.CompareTag("Chakra")
            )
        {
            GameObject.Find("GameManager").GetComponent<CurrencyManager>().EnergyHit();
        }
    }
}
