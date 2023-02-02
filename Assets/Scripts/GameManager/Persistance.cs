using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistance : MonoBehaviour
{
    public static Persistance i;
    
    void Awake () {
        if(i == null)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }else 
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
