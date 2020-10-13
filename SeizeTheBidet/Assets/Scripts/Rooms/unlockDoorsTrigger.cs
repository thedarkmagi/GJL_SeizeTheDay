using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockDoorsTrigger : MonoBehaviour
{
    public RoomScript room;
    public Material unlockedRoomMat;

    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(room!=null)
                room.openDoors=true;
            meshRenderer.material = unlockedRoomMat;
        }
    }
}
