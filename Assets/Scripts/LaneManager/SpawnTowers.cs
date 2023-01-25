using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnTowers : MonoBehaviour
{
    [SerializeField] private GameObject towers;
    [SerializeField] private GameObject tower;
    [SerializeField] private SOTower towerConfig;
    
    private IObjectPool<GameObject> towerPool; //TODO: Make this specific to Energy
    private void Awake()
    {
        towerPool = new ObjectPool<GameObject>(
            CreateTower,
            OnGetEnergy,
            OnReleaseEnergy,
            OnDestroyEnergy,
            maxSize: 20//TODO: Calculate how many should be on screen at any time, + 1 to be safe
        );
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnAllTowers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnAllTowers()
    {
        foreach (Transform _towerPosition in towers.transform)
        {
            GameObject _tower = towerPool.Get();
            _tower.GetComponent<TowerHealth>().SetTowerConfig(towerConfig);
            _tower.transform.position = _towerPosition.position;
            _tower.transform.SetParent(_towerPosition);
        }
        
    }

    #region PoolingSystem

    private GameObject CreateTower()
    {
        return Instantiate(tower);
    }

    private void OnGetEnergy(GameObject _tower)
    {
        _tower.SetActive(true);
        // _tower.transform.position = startPosition.transform.position;
        // _tower.transform.rotation = startPosition.transform.rotation;
        // _tower.GetComponent<Health>().SetHealth(soEnergyBasic.health); //TODO Make SO for towers
        // _tower.GetComponent<Movement>().SetEnergyPool(energyPool);
    }
    
    private void OnReleaseEnergy(GameObject _tower)
    {
        _tower.SetActive(false);
    }
    
    private void OnDestroyEnergy(GameObject _tower)
    {
        Destroy(_tower);
    }

    #endregion
}
