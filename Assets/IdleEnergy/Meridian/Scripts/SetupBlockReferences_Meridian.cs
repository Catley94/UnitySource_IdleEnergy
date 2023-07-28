using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupBlockReferences_Meridian : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject row in GameObject.FindGameObjectsWithTag("Blocks"))
        {
            foreach (Transform block in row.transform)
            {
                GetComponent<ParticleSystem>().trigger.AddCollider(block.GetComponent<Collider>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
