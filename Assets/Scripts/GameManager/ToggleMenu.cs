using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuButton;
    [SerializeField] private TMP_Text menuButtonText;
    [SerializeField] private GameObject menu;

    [SerializeField] private bool menuOpen = false;
    // Start is called before the first frame update
    public void ToggleShowHide()
    {
        menuOpen = !menuOpen;
        menu.SetActive(menuOpen);
        // menuButton.GetComponent<TMP_Text>().text = ToggleButtonCharacter();
        menuButtonText.text = ToggleButtonCharacter();
    }

    private string ToggleButtonCharacter()
    {
        return menuOpen ? "X" : "O";
    }
}
