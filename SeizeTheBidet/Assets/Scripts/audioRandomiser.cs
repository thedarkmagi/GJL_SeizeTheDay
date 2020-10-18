using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioRandomiser : MonoBehaviour
{
    public List<AudioClip> audioClips;
    AudioSource audioSource;
    public bool pitchRando;
    public float defaultPitch;
    public float pitchRandoAmount;
    

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void playClip()
    {
        if (audioClips != null)
        {
            audioSource.clip = randClip();
            if (pitchRando)
            {
                float randPitch = defaultPitch + Random.Range(0, pitchRandoAmount * 2) - pitchRandoAmount;
                audioSource.pitch = randPitch;
            }
            if(!audioSource.isPlaying)
                audioSource.Play();
        }
    }

    public void stopClip()
    {
        if (audioClips != null)
        {
            
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }


    public AudioClip randClip()
    {
        int i = Random.Range(0, audioClips.Count);
        return audioClips[i];
    }
}
