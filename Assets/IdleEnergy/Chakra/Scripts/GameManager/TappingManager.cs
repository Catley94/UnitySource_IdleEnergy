using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TappingManager : MonoBehaviour
{

    //TODO Duplicate variable in Upgrades.cs
    [SerializeField] private float tappingPower = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tap()
    {
        int roundNumber = 0;
        Transform rootChakraContainer = GameObject.FindWithTag("Blocks").transform.GetChild(roundNumber);
        
        foreach (Transform block in rootChakraContainer)
        {
            block.GetComponent<Health>().TakeDamage(tappingPower);
            //TODO: This will list all containers for Chakra Level blocks
            //TODO: Get round number from RoundManager
        }
    }

    public float GetTappingPower()
    {
        return tappingPower;
    }

    public void SetTappingPower(float amount)
    {
        tappingPower = amount;
    }
}
