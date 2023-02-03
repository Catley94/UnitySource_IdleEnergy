using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPersistance : MonoBehaviour
{
    public static CanvasPersistance i;
    
    void Awake () {
        if(i == null)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }else 
            Destroy(gameObject);
    }
}
