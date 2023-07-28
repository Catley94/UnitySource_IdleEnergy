using System.Collections;
using System.Collections.Generic;
using AssetKits.ParticleImage;
using UnityEngine;
using UnityEngine.Events;

public class UIParticle : MonoBehaviour
{
    
    public static UnityEvent OnParticleFinishEvent = new UnityEvent();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsPlaying()
    {
        return GetComponent<ParticleImage>().isPlaying;
    }

    public void Play()
    {
        GetComponent<ParticleImage>().Play();
    }

    public void Stop()
    {
        GetComponent<ParticleImage>().Stop();
    }

    public void ChangePosition(Vector2 position)
    {
        GetComponent<RectTransform>().anchoredPosition = position;
    }

    public void OnLastParticleFinish()
    {
        Debug.Log("FINISHED");
        OnParticleFinishEvent.Invoke();
    }

    public void SetParticleAmount(int amount)
    {
        GetComponent<ParticleImage>().rateOverLifetime = amount;
    }

}
