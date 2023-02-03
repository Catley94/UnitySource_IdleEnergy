using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
	[SerializeField] private int round = 1;
	[SerializeField] private int chakraRound = 1;
	
	public event Action onRoundReload;

	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextRound()
    {
	    bool triggerNextChakraRound = round == 7;
	    bool stayingOnCurrentChakra = round < 7;
	    if (triggerNextChakraRound)
	    {
		    IncreaseChakraRound();
		    ResetRound();
		    
	    }
	    else if(stayingOnCurrentChakra)
	    {
		    IncreaseRound();
	    }
	    
	    ReloadRound();
    }

    private void ReloadRound()
    {
	    onRoundReload?.Invoke();
    }

    public int GetRoundNumber()
	{
		return round;
	}

    public int GetChakraRoundNumber()
    {
	    return chakraRound;
    }

    private void ResetRound()
    {
	    round = 0;
    }

    private void IncreaseRound()
	{
		round += 1;
		Debug.Log("Round: " + round);
	}

	private void IncreaseChakraRound()
	{
		chakraRound += 1;
		Debug.Log("Chakra Round: " + round);
	}

	private void ResetChakraRound()
	{
		chakraRound = 0;
	}

}
