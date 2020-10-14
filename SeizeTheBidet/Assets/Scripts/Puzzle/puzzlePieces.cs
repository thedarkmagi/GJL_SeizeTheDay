using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzlePieces : MonoBehaviour
{

    public bool complete;
    public Material clearedMatt;

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
        if (other.tag == "Melee")
        {
            //Destroy(gameObject);// for now, apply a damage method here
            complete = true;
            meshRenderer.material = clearedMatt;
        }
    }
}
