using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Create Scriptable Objects/New Tower", order = 1)]
public class SOTower : ScriptableObject
{
    [SerializeField] public int baseTowerHealth;
}
