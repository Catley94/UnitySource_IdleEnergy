using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupBlockReferences : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform block in GameObject.FindWithTag("Blocks").transform)
        {
            GetComponent<ParticleSystem>().trigger.AddCollider(block.GetComponent<Collider>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
