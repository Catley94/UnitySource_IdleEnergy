using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private bool gameOver;
    [SerializeField] private bool chakraUnblocked;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private MoneyManagement _moneyManagement;
    [SerializeField] private ObjectCount _objectCount;
    [SerializeField] private HandleLives _handleLives;

    private bool no_money = false;
    private bool no_energy = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        SubToEvents();
    }

    private void SubToEvents()
    {
        _handleLives.CHAKRA_UNBLOCKED += SetChakraUnblocked;
        _moneyManagement.NO_MONEY += SetNoMoney;
        _objectCount.ENERGY += SetEnergy;
        _objectCount.NO_ENERGY += SetNoEnergy;
    }
    
    private void UnSubToEvents()
    {
        _handleLives.CHAKRA_UNBLOCKED -= SetChakraUnblocked;
        _moneyManagement.NO_MONEY -= SetNoMoney;
        _objectCount.ENERGY -= SetEnergy;
        _objectCount.NO_ENERGY -= SetNoEnergy;
    }

    private void SetChakraUnblocked()
    {
        chakraUnblocked = true;
        CheckGameWinLoseCondition();
    }

    private void SetNoMoney()
    {
        no_money = true;
        CheckGameWinLoseCondition();
    }

    private void SetEnergy()
    {
        no_energy = false;
    }

    private void SetNoEnergy()
    {
        no_energy = true;
        CheckGameWinLoseCondition();
    }

    private void CheckGameWinLoseCondition()
    {
        if (no_energy && no_money && !chakraUnblocked)
        {
            SetGameOver();
        } else if (chakraUnblocked)
        {
            SetGameWin();
        }
    }

    private void SetGameWin()
    {
        gameOver = true;
        SetGameOverText("YOU WON! RETURNING TO MAIN MENU");
        //TODO: Set text on screen to Chakra Unlocked, Game Won!
        //Transition to Main Menu scene
    }

    public void SetGameOver()
    {
        gameOver = true;
        SetGameOverText("GAME OVER, RETURNING TO MAIN MENU");
        //TODO: Set text on screen to GameOver
        //Transition to Main Menu scene
    }

    private void SetGameOverText(string text)
    {
        gameOverText.text = text;
    }

    private void OnDisable()
    {
        UnSubToEvents();
    }
}
