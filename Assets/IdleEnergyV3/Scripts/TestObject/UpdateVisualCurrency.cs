using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Idle_Engine.API;

public class UpdateVisualCurrency : MonoBehaviour
{
    
    [SerializeField] private TMP_Text currencyText;
    
    // Start is called before the first frame update
    void Start()
    {
        SubToEvents();
    }

    private void SubToEvents()
    {
        IE_IdleEngine.Instance.currencyTick.AddListener((tickObject) =>
        {
            Debug.Log("Tick");
            currencyText.text = tickObject.CurrencyAmount.ToString("F1");
        });   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
