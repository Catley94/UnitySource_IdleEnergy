using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speed : MonoBehaviour
{
    [SerializeField] private int gameSpeed = 1;
    [SerializeField] private Image _button;
    [SerializeField] private float multiplierReversion = 0.5f;
    [SerializeField] private int multiplier = 2;
    [SerializeField] private float currentMultiplier;

    public event Action<int> GameSpeedUpdated; 

    private void Start()
    {
        gameSpeed = 1;
    }

    public float GetGameSpeed()
    {
        return gameSpeed;
    }

    public void ToggleGameSpeed()
    {

        if (gameSpeed == 1)
        {
            gameSpeed = 2;
        }
        else
        {
            gameSpeed = 1;
        }

       
        GameSpeedUpdated?.Invoke(gameSpeed);
        _button.color = gameSpeed == 2 ? Color.green : Color.white;
    }
    
    
    
}
