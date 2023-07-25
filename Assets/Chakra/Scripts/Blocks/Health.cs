using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnParticleCollision(GameObject other)
    {
        health -= other.GetComponent<Damage>().GetDamage();
        if (health <= 0)
        {
            //Or Disable?
            Destroy(gameObject);
        }
    }
}
