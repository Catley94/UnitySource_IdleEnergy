using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Idle_Engine.Internal.Objects;

namespace Idle_Engine.Internal
{
    public class IE_CurrencyManager : MonoBehaviour
    {
    
        [SerializeField] private float currencyAmount;
    
        [SerializeField] private float currencyPerSecond;
        
        [SerializeField] private float tickEveryXSeconds = 1f;
    
        [SerializeField] private bool manuallyStartTick;
        
        public UnityEvent<TickObject> currencyTick = new UnityEvent<TickObject>();

        private bool tickActive = true;
        
        // Start is called before the first frame update
        void Start()
        {
            if(!manuallyStartTick) StartCurrencyTick();
        }

        #region Public

            public void StartCurrencyTick()
            {
                tickActive = true;
                StartCoroutine(CurrencyTick());
            }
        
            public void StopCurrencyTick()
            {
                tickActive = false;
                StopCoroutine(CurrencyTick());
            }
            
            public void IncreaseMoneyBy(float amount)
            {
                currencyAmount += amount;
                // currencyAmount = Mathf.Abs(currencyAmount);
            }
        
            public float GetTickRate()
            {
                return tickEveryXSeconds;
            }
            
            public void SetTickRate(float seconds)
            {
                tickEveryXSeconds = seconds;
            }
        
            public float GetCurrencyAmount()
            {
                return currencyAmount;
            }
        
            public void SetCurrencyAmount(float amount)
            {
                currencyAmount = amount;
            }
            
            public float GetCurrencyPerSecond()
            {
                return currencyPerSecond;
            }
            
            public void SetCurrencyPerSecond(float amount)
            {
                currencyPerSecond = amount;
            }

            public bool CheckIfCanAfford(float amount)
            {
                return currencyAmount >= amount;
            }
            
            public void DeductFromCurrency(float amount)
            {
                currencyAmount -= amount;
            }
            
        #endregion

        #region Private

            private IEnumerator CurrencyTick()
            {
                while (tickActive)
                {
                    yield return new WaitForSeconds(tickEveryXSeconds);
                    IncreaseMoneyBy(currencyPerSecond);
                    currencyTick?.Invoke(new TickObject(currencyAmount, currencyPerSecond));
                }
            }

        #endregion
        
    }
}


