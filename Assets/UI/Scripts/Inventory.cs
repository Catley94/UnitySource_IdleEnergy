using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private int itemCount;
    [SerializeField] private GameObject itemContainer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(string itemName)
    {
        //TODO: Convert param into an SO
        GameObject item = Instantiate(itemPrefab, itemContainer.transform);
        item.GetComponentInChildren<TMP_Text>().text = itemName + itemCount;
        itemCount++;
    }
}
