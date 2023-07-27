using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ModifyText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetBold(bool _bold)
    {
        if (_bold)
        {
            GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Bold;
        }
        else
        {
            GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Normal;
        }
    }
}
