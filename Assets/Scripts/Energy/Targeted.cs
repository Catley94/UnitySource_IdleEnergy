using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeted : MonoBehaviour
{
    [SerializeField] private bool targeted;

    public bool GetIsTargeted()
    {
        return targeted;
    }

    public void SetTargeted(bool _targeted)
    {
        targeted = _targeted;
    }
}
