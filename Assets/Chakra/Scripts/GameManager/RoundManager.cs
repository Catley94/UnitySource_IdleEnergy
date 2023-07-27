using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private int roundNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public int GetRound()
    {
        return roundNumber;
    }

    public void SetRound(int _roundNumber)
    {
        roundNumber = _roundNumber;
    }
}
