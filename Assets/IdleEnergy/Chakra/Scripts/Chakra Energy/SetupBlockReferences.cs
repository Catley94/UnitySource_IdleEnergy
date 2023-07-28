using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupBlockReferences : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int roundNumber = 0;
        Transform rootChakraContainer = GameObject.FindWithTag("Blocks").transform.GetChild(roundNumber);
        
        foreach (Transform block in rootChakraContainer)
        {
            GetComponent<ParticleSystem>().trigger.AddCollider(block.GetComponent<Collider>());
            //TODO: This will list all containers for Chakra Level blocks
            //TODO: Get round number from RoundManager
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
