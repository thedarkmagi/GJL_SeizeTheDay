using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct equipment
{
    public Mesh mesh;
    public Material material;
    public string tag;
}

public class Equipable : MonoBehaviour
{
    public equipment equip;
    audioRandomiser audio;
    bool firstTime;
    // Start is called before the first frame update
    void Start()
    {
        firstTime = false;
        audio = GetComponent<audioRandomiser>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("pickupMatches");
            var equip = collision.gameObject.GetComponentInChildren<MeleeAttack>();
            equip.setEquipment(this.equip);

            if (!firstTime)
            {
                if (audio != null)
                {
                    audio.playClip();
                }
                firstTime = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("pickupMatches");
            var equip = other.gameObject.GetComponentInChildren<MeleeAttack>();
            equip.setEquipment(this.equip);

            if (!firstTime)
            {
                if (audio != null)
                {
                    audio.playClip();
                }

                firstTime = true;
            }
        }
    }
}


