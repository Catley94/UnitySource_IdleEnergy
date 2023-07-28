using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private int roundNumber = 0;
    
    private SceneManager sceneManager;
    
    // Start is called before the first frame update
    void Start()
    {
        //TODO roundNumber = Load Saved data to get round number
        sceneManager = GameObject.FindWithTag("GameManager").GetComponent<SceneManager>();
        sceneManager.SaveBeforeLoad.AddListener(Save);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public int GetRound()
    {
        return roundNumber;
    }

    public void SetRound(int _roundNumber)
    {
        roundNumber = _roundNumber;
    }

    private void Save()
    {
        //Save Round Number
    }

    private void Load()
    {
        
    }

}
