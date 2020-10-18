using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePiss : MonoBehaviour
{
    public RoomScript room;
    public levelGeneration level;
    public GameObject Bros;



    public AudioClip pissClip;
    public AudioClip bidetClip;
    public AudioClip toiletClip;
    bool oneTimeOnly;

    AudioSource audio;
        // Start is called before the first frame update
    void Start()
    {
       audio = GetComponent<AudioSource>();
        oneTimeOnly = false;

       level = GameObject.FindObjectOfType<levelGeneration>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //if (level != null)
            //    level.postPee();
            if (!oneTimeOnly)
            {
                room.closeDoors();
                gameController.instance.pre_pee = false;

                other.gameObject.GetComponent<FirstPersonAIO>().playerCanMove = false;
                StartCoroutine(playpee());
                gameController.instance.depletePissMeter(Bros);
                oneTimeOnly = true;
            }
        }

    }


    IEnumerator playpee()
    {
        audio.clip = pissClip;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = bidetClip;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = toiletClip;
        audio.Play();
        //yield return new WaitForSeconds(audio.clip.length);

        gameController.instance.peeFinished = true;
    }



}
