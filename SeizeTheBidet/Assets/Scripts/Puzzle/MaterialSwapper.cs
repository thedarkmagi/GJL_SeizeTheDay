using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
    public Material cleanMatt;
    public Material messyMatt;

    public MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMessyMatt(bool isMessy)
    {
        if(isMessy)
        {
            meshRenderer.material = messyMatt;
        }
        else
        {
            meshRenderer.material = cleanMatt;
        }
    }

}
