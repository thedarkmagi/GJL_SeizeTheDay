using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePiss : MonoBehaviour
{
    public RoomScript room;
    public levelGeneration level;
    // Start is called before the first frame update
    void Start()
    {
        level = GameObject.FindObjectOfType<levelGeneration>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //if (level != null)
            //    level.postPee();
            
            room.closeDoors();
            gameController.instance.pre_pee = false;
        }

    }
}
