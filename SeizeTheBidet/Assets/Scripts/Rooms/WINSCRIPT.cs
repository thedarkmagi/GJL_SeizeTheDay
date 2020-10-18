using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WINSCRIPT : MonoBehaviour
{


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
                //OpenDoor();
                //if (adjencentDoor != null)
                //    adjencentDoor.OpenDoor();
                //do a win
                    gameController.instance.activateWinScreen();
                }
            }


    }

    private void OnTriggerStay(Collider other)
    {


    }
}
