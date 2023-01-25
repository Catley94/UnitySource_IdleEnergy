using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
	[SerializeField] private int round = 1;
	[SerializeField] private int chakraRound = 1;

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
	    if (round == 7)
	    {
		    IncreaseChakraRound();
		    ResetRound();
	    }
	    else if(round < 7)
	    {
		    IncreaseRound();
	    }
	    GameObject.Find("GameManager").GetComponent<Scenes>().LoadSceneByName("InGame");
    }

    private int GetRoundNumber()
	{
		return round;
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
