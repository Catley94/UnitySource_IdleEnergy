using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneManager : MonoBehaviour
{

    public UnityEvent SaveBeforeLoad;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void MoveToMeridian()
    {
        SaveBeforeLoad?.Invoke();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Meridian");
    }
    
    public void MoveToChakras()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Chakras");
    }


}
