using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject chakra;
    private int trackPointIndex = 0;
    [SerializeField] private SOEnergy soEnergy;
    [SerializeField] private GameObject allTracks; //Rename to tracks
    [SerializeField] private GameObject selectedTrack;
    [SerializeField] private GameObject leftTrack; 
    [SerializeField] private GameObject midTrack; 
    [SerializeField] private GameObject rightTrack; 
    [FormerlySerializedAs("pathway")] [SerializeField] private GameObject track;

    [FormerlySerializedAs("pathways")] [SerializeField] private GameObject[] tracks;

    [SerializeField] private int count = 0;

    [FormerlySerializedAs("speed")] [SerializeField] private float baseSpeed = 0.1f;
    private float defaultSpeed;

    [SerializeField] private Vector3 startingPos;
    
    // Start is called before the first frame update
    void Start()
    {
        SetStartingPosition(transform.position);
        SetSpeed();
        SubEvents();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, tracks[count].transform.position) < 1)
        {
            if(count <= tracks.Length) count++;
        }
        transform.position = Vector3.MoveTowards(transform.position, tracks[count].transform.position, baseSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chakra"))
        {
            Debug.Log("Energy absorbed");
            ReturnToPool();
            count = 0;
        }
    }
    
    private void SetupPathway(GameObject _selectedTrack)
    {
        foreach (Transform trackPoint in _selectedTrack.transform)
        {
            tracks[trackPointIndex] = trackPoint.gameObject;
            Debug.Log(trackPoint);
            trackPointIndex++;
        }
        
        if (trackPointIndex == _selectedTrack.transform.childCount)
        {
            tracks[trackPointIndex] = chakra.gameObject;
        }
        
    }

    private void SubEvents()
    {
        GameObject.Find("GameManager").GetComponent<Speed>().GameSpeedUpdated += SetGameSpeed;
    }

    private void SetGameSpeed(int _multiplier)
    {
        if (_multiplier == 2)
        {
            baseSpeed *= _multiplier;
        }
        else
        {
            baseSpeed = defaultSpeed;
        }
    }

    private void UnsubEvents()
    {
        GameObject.Find("GameManager").GetComponent<Speed>().GameSpeedUpdated -= SetGameSpeed;
    }

    private void ReturnToPool()
    {
        Debug.Log("Returned to Pool");
        Destroy(gameObject);
        //TODO Return to pool
        UnsubEvents();
    }

    public void SetTrack(int _trackIndex)
    {
        GameObject _track = allTracks.transform.GetChild(_trackIndex).gameObject;
        selectedTrack = _track;
        Debug.Log(selectedTrack);
        SetupPathway(selectedTrack);
        switch (_trackIndex)
        {
            case 0:
                SetStartingPosition(new Vector3(transform.position.x - 3, transform.position.y, transform.position.z));
                transform.position = startingPos;
                break;
            case 1:
                SetStartingPosition(transform.position);
                transform.position = startingPos;
                break;
            case 2:
                SetStartingPosition(new Vector3(transform.position.x + 3, transform.position.y, transform.position.z));
                transform.position = startingPos;
                break;
            default:
                break;
        }
    }

    public void SetSpeed()
    {
        baseSpeed = baseSpeed *= GameObject.Find("GameManager").GetComponent<Speed>().GetGameSpeed() * soEnergy.speed;
        defaultSpeed = baseSpeed;
    }
    
    private void SetStartingPosition(Vector3 startPos)
    {
        startingPos = startPos;
        transform.position = startPos;
    }

    public void SetSOEnergy(SOEnergy _soEnergy)
    {
        soEnergy = _soEnergy;
    }
}
