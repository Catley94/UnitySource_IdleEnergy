using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UnlockLane : MonoBehaviour, IUpgrade
{
    [SerializeField] private GameObject unlockLane;
    [SerializeField] private int level = 1;
    [SerializeField] private float price = 1f;
    [SerializeField] private GameObject lanes;
    [SerializeField] private GameObject unlockLaneButton;
    [SerializeField] private GameObject priceLabel;
    private Purchasing purchasing;
    
    public event Action onUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        purchasing = GetComponent<Purchasing>();
        SubToEvents();
        unlockLane.transform.Find("Level").GetComponent<TMP_Text>().text = level.ToString();
        unlockLane.transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
    }
    
    private void SubToEvents()
    {
        purchasing.onLaneUnlock += OnPurchase;
    }
    
    public void OnPurchase()
    {
        if (level <= 7)
        {
            level += 1;
            price += (price * 0.1f);
            unlockLane.transform.Find("Level").GetComponent<TMP_Text>().text = level.ToString();
            if (level == 7)
            {
                unlockLane.transform.Find("Price").GetComponent<TMP_Text>().text = "";
                priceLabel.SetActive(false);
                unlockLaneButton.SetActive(false);
            }
            else
            {
                unlockLane.transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
            }
            
            onUpgrade?.Invoke();
            //Gets all disabled lanes, then enables the [0] in List<GameObject>
            //When level > 8, Redundant call, should be within a condition to prevent this from being 
            UnlockNextLane(); 
        }
        else
        {
            unlockLane.transform.Find("Price").GetComponent<TMP_Text>().text = "";
            unlockLaneButton.SetActive(false);
        }
    }

    //There is a Monobehaviour.Reset which could be useful
    //Though not sure if it's purely used for the Inspector or can be used in game
    private void ResetValues()
    {
        level = 1;
        price = 1;
        priceLabel.SetActive(true);
        unlockLaneButton.SetActive(true);
        unlockLane.transform.Find("Level").GetComponent<TMP_Text>().text = level.ToString();
        unlockLane.transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
    }

    public int GetLevel()
    {
        return level;
    }

    public float GetPrice()
    {
        return price;
    }

    private void UnlockNextLane()
    {
        List<GameObject> lockedLanes = new List<GameObject>();
        for (int i = 0; i < lanes.transform.childCount; i++)
        {
            GameObject child = lanes.transform.GetChild(i).gameObject;
            if (!child.activeInHierarchy)
            {
                lockedLanes.Add(lanes.transform.GetChild(i).gameObject);
            }
        }

        if (lockedLanes.Count > 0)
        {
            lockedLanes[0].SetActive(true);
        }
    }


}
