using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoomPos : MonoBehaviour
{
    public GameObject spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform getSpawnPos()
    {
        return spawnPos.transform;
    }
}
