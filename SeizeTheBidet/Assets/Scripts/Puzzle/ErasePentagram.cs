using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErasePentagram : MonoBehaviour
{
    Vector3 playerLastPos;

    public float amountNeedWalkingOn;
    public float speedMulti=1;
    puzzlePieces puzzlePieces;

    public MaterialSwapper roomMattswap;

    audioRandomiser audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<audioRandomiser>();
        puzzlePieces = GetComponent<puzzlePieces>();
    }

    // Update is called once per frame
    void Update()
    {
        if(amountNeedWalkingOn<0)
        {
            puzzlePieces.complete = true;
            roomMattswap.setMessyMatt(true);
            audio.stopClip();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(playerLastPos!=null)
            {
                if(playerLastPos != other.gameObject.transform.position)
                {
                    amountNeedWalkingOn -= Time.deltaTime * speedMulti;
                    if(!puzzlePieces.complete)
                    {
                        audio.playClip();
                    }
                }
            }
            else
            {
                playerLastPos = other.gameObject.transform.position;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerLastPos != null)
            {
                if (playerLastPos != other.gameObject.transform.position)
                {
                    amountNeedWalkingOn -= Time.deltaTime * speedMulti;
                    if (!puzzlePieces.complete)
                    {
                        audio.playClip();
                    }
                }
            }
            else
            {
                playerLastPos = other.gameObject.transform.position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
