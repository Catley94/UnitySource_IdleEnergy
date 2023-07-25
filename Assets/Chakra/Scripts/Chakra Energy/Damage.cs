using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private float damage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public float GetDamage()
    {
        return damage;
    }
    
    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
}
