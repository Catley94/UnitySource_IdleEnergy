using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    [SerializeField] private int health = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        Debug.Log("Tower Destroyed");
        Destroy(gameObject);
    }
}
