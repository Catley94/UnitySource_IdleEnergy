using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandleLives : MonoBehaviour
{
    private ObjectCount _objectCount;
    [SerializeField] private int lifeCount;

    [SerializeField] private TMP_Text lifeCountText;

    public event Action CHAKRA_UNBLOCKED;
    // Start is called before the first frame update
    void Start()
    {
        lifeCount = 200;
        UpdateLifeCountText();
        _objectCount = GameObject.Find("GameManager").GetComponent<ObjectCount>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Energy"))
        {
            lifeCount--;
            UpdateLifeCountText();
            _objectCount.Remove(other.gameObject);
        }
    }

    private void UpdateLifeCountText()
    {
        lifeCountText.text = lifeCount.ToString();
        if (lifeCount <= 0)
        {
            Debug.Log("Chakra is now spinning!");
            CHAKRA_UNBLOCKED?.Invoke();
        }
    }
}
