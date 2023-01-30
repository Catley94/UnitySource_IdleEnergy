using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChanger : MonoBehaviour
{
    private RoundManager _roundManager;
    // Start is called before the first frame update
    void OnEnable()
    {
        _roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
        
        switch (_roundManager.GetChakraRoundNumber())
        {
            case 1:
                SetColour(Color.red);
                break;
            case 2:
                SetColour(Color.white);
                break;
            case 3:
                SetColour(Color.yellow);
                break;
            case 4:
                SetColour(Color.green);
                break;
            case 5:
                SetColour(Color.cyan);
                break;
            case 6:
                SetColour(Color.blue);
                break;
            case 7:
                SetColour(Color.magenta);
                break;
        }
    }

    private void SetColour(Color _colour)
    {
        GetComponent<MeshRenderer>().material.color = _colour;
    }
}
