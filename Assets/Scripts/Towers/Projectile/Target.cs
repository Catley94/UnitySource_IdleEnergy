using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private bool hasTarget = false;

    [SerializeField] private GameObject target = null;

    [SerializeField] private int speed = 20;
    private int defaultSpeed;

    private void Start()
    {
        defaultSpeed = speed;
        SubEvents();
    }

    

    private void SetGameSpeed(int _multiplier)
    {
        if (_multiplier == 2)
        {
            speed *= _multiplier;
        }
        else
        {
            speed = defaultSpeed;
        }
    }

    public bool GetHasTarget()
    {
        return hasTarget;
    }
    
    public void SetHasTarget(bool _targeted)
    {
        hasTarget = hasTarget;
    }
    
    public GameObject GetTarget()
    {
        return target;
    }
    
    public bool HasTarget()
    {
        return target;
    }
    
    public void SetTarget(GameObject _target)
    {
        target = _target;
    }

    private void Update()
    {
        if(HasTarget())transform.position = Vector3.MoveTowards(transform.position, GetTarget().transform.position,speed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Energy"))
        {
            GameObject energy = other.gameObject;
            if (energy == GetTarget())
            {
                energy.GetComponent<Health>().TakeDamage();
                ReturnBackToPool();
            }
        }
    }

    private void ReturnBackToPool()
    {
        UnsubEvents();
        //TODO: Return back to pool, do not destroy
        Destroy(gameObject);
    }
    
    private void SubEvents()
    {
        GameObject.Find("GameManager").GetComponent<Speed>().GameSpeedUpdated += SetGameSpeed;
    }

    private void UnsubEvents()
    {
        GameObject.Find("GameManager").GetComponent<Speed>().GameSpeedUpdated -= SetGameSpeed;
    }
    
}
