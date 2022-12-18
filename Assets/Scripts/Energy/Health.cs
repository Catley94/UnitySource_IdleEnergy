using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private ObjectCount _objectCount;
    [SerializeField] private int health = -1;
    [SerializeField] private Material material;
    [SerializeField] private float percentage;

    private void Start()
    {
        _objectCount = GameObject.Find("GameManager").GetComponent<ObjectCount>();
    }
    
    // Update is called once per frame
    void Update()
    {
        material.color = Color.Lerp(material.color, Color.gray, percentage);
    }

    public void TakeDamage()
    {
        health--;
        material.color = Color.red;
        Debug.Log("Taken damage");
        if(health == 0) ReturnToPool();
    }

    public void SetHealth(int _health)
    {
        health = _health;
    }
    
    private void ReturnToPool()
    {
        Debug.Log("Returned to Pool");
        Destroy(gameObject);
        //TODO Return to pool
        _objectCount.Remove(gameObject);
    }
}
