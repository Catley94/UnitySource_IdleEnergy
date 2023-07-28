using System;
using System.Collections;
using System.Collections.Generic;
using AssetKits.ParticleImage;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private int currencyWorth = 1; 
    private CurrencyManager currencyManager;
    private UIParticle particleImage;
    
    RectTransform particleImageRect;
    
    // Start is called before the first frame update
    void Start()
    {
        currencyManager = GameObject.FindWithTag("GameManager").GetComponent<CurrencyManager>();
        particleImage = GameObject.FindWithTag("ParticleImage1").GetComponent<UIParticle>();
    }

    private void OnParticleCollision(GameObject other)
    {
        health -= other.GetComponent<Damage>().GetDamage();
        CheckIfHealthLessThanZero();
    }

    private void CheckIfHealthLessThanZero()
    {
        if (health <= 0)
        {
            PlayCurrencyParticle();

            DisableMeshAndCollider();

            UIParticle.OnParticleFinishEvent.AddListener(() => Destroy(gameObject));
        }
    }

    private void PlayCurrencyParticle()
    {
        particleImageRect = particleImage.GetComponent<RectTransform>();
        
        RectTransform CanvasRect = GameObject.FindWithTag("Canvas").GetComponent<RectTransform>();

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));
        
        particleImage.SetParticleAmount(currencyWorth);
        particleImage.ChangePosition(WorldObject_ScreenPosition);
        particleImage.Play();
    }

    private void DisableMeshAndCollider()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }

    private void OnDestroy()
    {
        currencyManager.IncreaseMoneyBy(currencyWorth);
        //TODO: Instantiate (or pool) a new Particle Image which spawns at the location of this object in canvas.
        //This way more than one object can be destroyed at a time and the particle image will spawn at the correct location.
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        CheckIfHealthLessThanZero();
    }
}
