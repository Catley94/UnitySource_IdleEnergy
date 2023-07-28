using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private int blockCount;

    public UnityEvent allBlocksDefeated;

    private void Start()
    {
        blockCount = GameObject.FindWithTag("GameManager")
            .GetComponent<Config>().GetConfig()
            .roundConfig[GameObject.FindWithTag("GameManager").GetComponent<RoundManager>().GetRound()]
            .blockCount;
    }

    public void BlockDefeated()
    {
        blockCount -= 1;
        if(blockCount == 0) allBlocksDefeated?.Invoke();
    }
}
