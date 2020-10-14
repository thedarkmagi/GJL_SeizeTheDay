using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    //Variables;
    Animator anim;

    public equipment currentEquipment;

    MeshFilter mesh;
    MeshRenderer mRenderer;
    BoxCollider collider;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>();
        mRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();

        disableEquipment();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            anim.SetBool("attacking", true);
        else if (Input.GetButtonUp("Fire1"))
            anim.SetBool("attacking", false);
    }


    public void setEquipment(equipment newEquip)
    {
        currentEquipment = newEquip;
        mRenderer.enabled =true;
        collider.enabled = true;

        mesh.mesh = currentEquipment.mesh;
        mRenderer.material = currentEquipment.material;
        //gameObject.tag = currentEquipment.tag;
    }

    public void disableEquipment()
    {
        //mesh.
        mRenderer.enabled = false;
        collider.enabled = false;
    }
}
