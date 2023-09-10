using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Idle_Engine.Internal.Objects
{
    public class TickObject
    {

        public float CurrencyAmount;
        public float CurrencyPerSecond;
        
        public TickObject(float currencyAmount, float currencyPerSecond)
        {
            CurrencyAmount = currencyAmount;
            CurrencyPerSecond = currencyPerSecond;
        }
    }
}

