using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Idle_Engine.Internal;
using Idle_Engine.Internal.Objects;

namespace Idle_Engine.API
{

    public class IE_IdleEngine : MonoBehaviour
    {

        public UnityEvent<TickObject> currencyTick = new UnityEvent<TickObject>();
        private IE_CurrencyManager _ieCurrencyManager;
        
        public static IE_IdleEngine Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        // Start is called before the first frame update
        void Start()
        {
            SetupReferences();
            SubToEvents();
        }

        #region Public

            public void StartCurrencyTick()
            {
                _ieCurrencyManager.StartCurrencyTick();
            }

            public void PauseCurrencyTick() => StopCurrencyTick();
            public void StopCurrencyTick()
            {
                _ieCurrencyManager.StopCurrencyTick();
            }

            public float GetCurrencyAmount()
            {
                return _ieCurrencyManager.GetCurrencyAmount();
            }

            public void SetCurrencyAmount(float amount)
            {
                //Setting an absolute currency value - can be destructive
                _ieCurrencyManager.SetCurrencyAmount(amount);
            }

            public float GetTickRateInSeconds()
            {
                return _ieCurrencyManager.GetTickRate();
            }
            
            public void SetTickRateInSeconds(float amount)
            {
                //Frequency the currency is generated, every ... seconds
                _ieCurrencyManager.SetTickRate(amount);
            }

            public float GetCurrencyPerSecond()
            {
                return _ieCurrencyManager.GetCurrencyPerSecond();
            }

            public void SetCurrencyPerSecond(float amount)
            {
                //How much currency is generated per tick - can be destructive
                _ieCurrencyManager.SetCurrencyPerSecond(amount);
            }
            
            public void IncreaseCurrencyPerSecondBy(float amount)
            {
                //Increase the amount of currency generated per tick
                SetCurrencyPerSecond(GetCurrencyPerSecond() + amount);
            }
            
            public void GetCurrencyAmountInXSeconds(float seconds)
            {
                //How much currency will be generated in X seconds
            }

            public void SetNotificationInXSeconds(string message)
            {
                //Set a notification to be displayed in X seconds
            }

            public void PurchaseWithAmount(float amount)
            {
                if (_ieCurrencyManager.CheckIfCanAfford(amount))
                {
                    _ieCurrencyManager.DeductFromCurrency(amount);
                }

            }

            #endregion

        #region Private

            private void SetupReferences()
            {
                _ieCurrencyManager = GetComponent<IE_CurrencyManager>();
            }
        
            private void SubToEvents()
            {
                _ieCurrencyManager.currencyTick.AddListener(OnCurrencyTick);   
            }

            private void OnCurrencyTick(TickObject tickObject)
            {
                currencyTick?.Invoke(tickObject);
            }
            
        #endregion

    }
}


