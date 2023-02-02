using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (GameObject.Find("GameManager").activeInHierarchy)
        {
            
        }
        else
        {
            Instantiate(gameManager);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
