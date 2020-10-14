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

    private void Start()
    {
        mesh = GetComponent<MeshFilter>();
        mRenderer = GetComponent<MeshRenderer>();
        anim = GetComponent<Animator>();

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
        mesh.mesh = currentEquipment.mesh;
        mRenderer.material = currentEquipment.material;
        //gameObject.tag = currentEquipment.tag;
    }
}
