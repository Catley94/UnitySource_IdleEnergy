using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    
    private SceneManager sceneManager;
    
    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.FindWithTag("GameManager").GetComponent<SceneManager>();
        sceneManager.SaveBeforeLoad.AddListener(Save);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void Save()
    {
        //Save money
    }

    private void Load()
    {
        
    }
}
