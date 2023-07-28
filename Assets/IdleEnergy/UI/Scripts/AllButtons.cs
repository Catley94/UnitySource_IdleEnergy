using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HideAllButtons()
    {
        foreach (Transform buttonGroup in transform)
        {
            buttonGroup.gameObject.SetActive(false);
        }
    }
    
    public void ShowButtonGroup(int _buttonGroup)
    {
        HideAllButtons();
        transform.GetChild(_buttonGroup).gameObject.SetActive(true);
    }
}
