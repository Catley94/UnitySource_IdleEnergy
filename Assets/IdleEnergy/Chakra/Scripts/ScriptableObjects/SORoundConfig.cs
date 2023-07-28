using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Round Config", menuName = "Scriptable Objects/New Round Config", order = 1)]
public class SORoundConfig : ScriptableObject
{
    public RoundConfig[] roundConfig;
}
