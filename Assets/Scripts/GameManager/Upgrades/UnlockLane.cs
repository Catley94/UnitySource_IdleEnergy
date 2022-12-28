using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnlockLane : MonoBehaviour, IUpgrade
{
    [SerializeField] private GameObject unlockLane;
    [SerializeField] private int level = 1;
    [SerializeField] private float price = 1f;
    [SerializeField] private GameObject lanes;
    private Purchasing purchasing;
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
        level += 1;
        price += (price * 0.1f);
        unlockLane.transform.Find("Level").GetComponent<TMP_Text>().text = level.ToString();
        unlockLane.transform.Find("Price").GetComponent<TMP_Text>().text = price.ToString();
        bool laneUnlocked = UnlockNextLane();
    }

    public int GetLevel()
    {
        return level;
    }

    public float GetPrice()
    {
        return price;
    }

    private bool UnlockNextLane()
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
            return true;
        }
        else
        {
            return false;
        }
    }


}
