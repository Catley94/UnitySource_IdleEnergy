using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgrade
{
    int GetLevel();
    float GetPrice();
    void OnPurchase() {}
}
