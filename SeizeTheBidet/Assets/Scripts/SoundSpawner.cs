using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSpawner : MonoBehaviour
{
    [SerializeField] public GameObject customObject;

    // Start is called before the first frame update
    void OnTriggerEnter (Collider Player)
    {
        customObject.SetActive(true);
    }

    void OnTriggerExit(Collider Player)
    {
        customObject.SetActive(false);
    }

}
