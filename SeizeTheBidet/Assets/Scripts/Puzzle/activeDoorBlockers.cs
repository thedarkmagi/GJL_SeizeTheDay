using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeDoorBlockers : MonoBehaviour
{
    public PuzzleController roomPuzzle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameController.instance.pre_pee)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (roomPuzzle != null)
                {
                    //room.closeDoors();
                    //room.openDoors = true;
                    roomPuzzle.BlockRoom(true);
                    Debug.Log("blockRooms?");
                }

            }
        }
    }
}
