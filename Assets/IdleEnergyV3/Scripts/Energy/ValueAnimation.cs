using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Idle_Engine.API;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ValueAnimation : MonoBehaviour
{
    
    [FormerlySerializedAs("value")] [SerializeField] private float xValue;
    [SerializeField] private float xDuration;
    [SerializeField] private float yValue;
    [SerializeField] private float yDuration;
    
    private TMP_Text textToTween;
    private Vector3 originalPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        SetupStartPosition();
        SubToEvents();
        SetupReferences();
        // Play();
    }

    private void SetupStartPosition()
    {
        originalPosition = transform.position;
    }

    private void SubToEvents()
    {
        IE_IdleEngine.Instance.currencyTick.AddListener((currencyAmount) =>
        {
            Debug.Log("Tick");
            Play();
        });
    }

    private void SetupReferences()
    {
        textToTween = GetComponentInChildren<TMP_Text>();
    }
    
    public void Play()
    {
        PlayXAnimation();
        
        PlayYAnimation();
        
        PlayFadeAnimation();
    }

    private void PlayXAnimation()
    {
        transform.DOLocalMoveX(xValue, xDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(4, LoopType.Yoyo);
    }
    
    private void PlayYAnimation()
    {
        transform.DOLocalMoveY(yValue, yDuration)
            .SetEase(Ease.InOutSine);
    }
   
    private void PlayFadeAnimation()
    {
        textToTween.DOFade(0f, 1f)
            .OnComplete(() =>
            {
                ResetPosition();
                ResetAlpha();
            });
    }

    private void ResetAlpha()
    {
        Color textColor = GetComponentInChildren<TMP_Text>().color;
        textColor.a = 1f;
        GetComponentInChildren<TMP_Text>().color = textColor;
    }

    private void ResetPosition()
    {
        transform.position = originalPosition;
    }
}
