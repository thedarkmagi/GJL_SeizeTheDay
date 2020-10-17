using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLockUnlock : MonoBehaviour
{
    public FirstPersonAIO firstPerson;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void lockMovement()
    {
        firstPerson.playerCanMove = false;
        Debug.Log("are lock movemenet??");
    }

    public void unlockMovement()
    {
        firstPerson.playerCanMove = true;
        Debug.Log("are unlock movemenet??");
    }
}
