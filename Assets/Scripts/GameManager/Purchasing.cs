using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Purchasing : MonoBehaviour
{
    public event Action onEnergyValueUpgrade;
    public event Action onEnergySpawnRateUpgrade;
    public event Action onLaneUnlock;
    public event Action onChakraUnlock;
    
    public void PurchaseEnergyValue() => onEnergyValueUpgrade?.Invoke();
    public void PurchaseEnergySpawnRate() => onEnergySpawnRateUpgrade?.Invoke();
    public void PurchaseUnlockPath() => onLaneUnlock?.Invoke();
    public void PurchaseUnlockChakra() => onChakraUnlock?.Invoke();
}

