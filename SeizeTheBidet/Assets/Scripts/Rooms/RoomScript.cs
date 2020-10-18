using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public List<DoorScript> doors;

    public bool openDoors;


    audioRandomiser audio;
    bool firstTime;
    // Start is called before the first frame update
    void Start()
    {
        firstTime = false;
        audio = GetComponent<audioRandomiser>();
    }

    // Update is called once per frame
    void Update()
    {
        if(openDoors)
        {
            unlockDoors();
            if(!firstTime)
            {
                if(audio!=null)
                {
                    audio.playClip();
                }

                firstTime = true;
            }
        }
    }


    public void unlockDoors()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].doorOpenable = true;
        }
    }

    public void lockDoors()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].CloseDoor();
            doors[i].doorOpenable = false;
        }
    }
    public void closeDoors()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].CloseDoor();
        }
    }

}
