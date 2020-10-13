using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private const int MaxDistance = 1;
    public bool doorOpenable = true;

    DoorScript adjencentDoor;

    bool firstFrame;

    // Start is called before the first frame update
    void Start()
    {
        firstFrame = true;
        findAdjacentDoor();
    }

    // Update is called once per frame
    void Update()
    {
        if(firstFrame)
        {
            findAdjacentDoor();
            firstFrame = false;
        }
    }

    public void OpenDoor()
    {
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }

    public void CloseDoor()
    {
        this.gameObject.SetActive(true);
    }

    public void findAdjacentDoor()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 bkwd = transform.TransformDirection(-Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, MaxDistance))
        {
            if (hit.collider.gameObject.CompareTag("Door"))
            {
                adjencentDoor = hit.collider.gameObject.GetComponent<DoorScript>();
            }
            //print("There is something in front of the object!");
        }
        if (Physics.Raycast(transform.position, bkwd, out hit, MaxDistance))
        {
            if (hit.collider.gameObject.CompareTag("Door"))
            {
                adjencentDoor = hit.collider.gameObject.GetComponent<DoorScript>();
            }
            //print("There is something in behind of the object!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (doorOpenable)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OpenDoor();
                if(adjencentDoor!=null)
                    adjencentDoor.OpenDoor();
            }
        }
        if (other.gameObject.CompareTag("Door"))
        {
            adjencentDoor = other.gameObject.GetComponent<DoorScript>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            adjencentDoor = other.gameObject.GetComponent<DoorScript>();
        }

    }
}
