using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Idle_Engine.API;

public class ScaleAnimation : MonoBehaviour
{

    [SerializeField] private float animationTime;
    
    // Start is called before the first frame update
    void Start()
    {
        SubToEvents();
    }

    private void SubToEvents()
    {
        IE_IdleEngine.Instance.currencyTick.AddListener((currencyAmount) =>
        {
            Debug.Log("Tick");
            transform.DOScale(2f, animationTime).onComplete += () => transform.DOScale(1f, animationTime);
        });
    }
    
}
