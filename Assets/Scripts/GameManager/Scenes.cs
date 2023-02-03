using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    private RoundManager roundManager;
    // Start is called before the first frame update
    void Start()
    {
        roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
        roundManager.onRoundReload += () => LoadSceneByName("InGame");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneByName(string sceneName)
    {
        // SceneManager.LoadScene(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
