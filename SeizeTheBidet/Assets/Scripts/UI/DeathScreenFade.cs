
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenFade : MonoBehaviour
{
    Image lightFade;
    public bool fade;
    Text deathtextObject;
    public string gameOverText;
    //List<CharacterRespawner> allRespawn;
    // Start is called before the first frame update
    void Start()
    {
        fade = false;
        if (TryGetComponent(out Image component))
        {
            lightFade = component;
        }
        
        deathtextObject = GetComponentInChildren<Text>();
        deathtextObject.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(fade)
        {
            lightFade.color = new Color(lightFade.color.r, lightFade.color.g, lightFade.color.b, Mathf.Lerp(lightFade.color.a, 1, 0.1f));
            if(lightFade.color.a >= 0.9f)
            {
                //deathtextObject.text = gameOverText + "\n " + ScoreSystem.instance.text+ ScoreSystem.instance.score +"\n Any key to respawn";
                deathtextObject.enabled = true;
                if(Input.anyKeyDown)
                {
                    fade = false;
                    deathtextObject.enabled = false;
                    //ScoreSystem.instance.score = 0;
                    //ScoreSystem.instance.incrementScore(0);

                    //I hate doing this but gotta get something working first
                    //var player = GameObject.Find("Spacefish3");


                    //for (int i = 0; i < allRespawn.Count; i++)
                    //{
                    //    allRespawn[i].Respawn();
                    //}

                    //var respawner = player.GetComponent<CharacterRespawner>();
                    //respawner.Respawn();
                    gameController.instance.loadAnyScene("Menu");
                }
            }
        }
        else
        {
            lightFade.color = new Color(lightFade.color.r, lightFade.color.g, lightFade.color.b, Mathf.Lerp(lightFade.color.a, 0, 0.1f));
            deathtextObject.enabled = false;
        }
    }


    public void activeFade()
    {
        fade = true;
    }
}
