using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject block;

    // Start is called before the first frame update
    void Awake()
    {
        //TODO: Get Round number from RoundManager
        int roundNumber = 0;
        Transform chakraLevelBlocksContainer = transform.GetChild(roundNumber);
        foreach (Transform block in chakraLevelBlocksContainer)
        {
            block.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
