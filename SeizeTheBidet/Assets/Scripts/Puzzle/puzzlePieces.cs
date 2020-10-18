using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzlePieces : MonoBehaviour
{

    public bool complete;
    public Mesh clearedMesh;
    public string tag= "Melee";

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    public GameObject subObject;

    audioRandomiser audio;
    bool firstTime;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();

        firstTime = false;
        audio = GetComponent<audioRandomiser>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            if (other.GetComponent<MeleeAttack>().currentEquipment.tag == tag)
            {
                //Destroy(gameObject);// for now, apply a damage method here
                complete = true;
                //meshRenderer.material = clearedMatt;

                if (!firstTime)
                {
                    if (audio != null)
                    {
                        audio.playClip();
                    }
                    firstTime = true;
                }

                if (clearedMesh!=null)
                    meshFilter.mesh = clearedMesh;
                if(subObject!=null)
                {
                    subObject.SetActive(true);
                }
            }
        }
    }
}
