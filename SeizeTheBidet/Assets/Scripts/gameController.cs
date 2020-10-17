﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class gameController : MonoBehaviour
{
    public static gameController instance;

    public bool pre_pee=true;


    public float Pee_maxTimeTillGameOver;
    public Slider slider;
    public Color anxityColour;
    public Image sliderFill;
    public Image sliderBackground;
    float Pee_curTimeTillGameOver;

    public Sprite anxityMeter;
    public Sprite anxityBackground;

    public float anxity_maxTimeTillGameOver;
    float anixty_curTimeTillGameOver;

    public DeathScreenFade gameOverUI;
    public DeathScreenFade winUI;

    public float peeDecreaseSpeed;

    public GameObject Player;
    FirstPersonAIO firstPerson;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one gameController in the scene");
        }
        else
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Pee_curTimeTillGameOver = 0;
        slider.maxValue = Pee_maxTimeTillGameOver;
        firstPerson = Player.GetComponent<FirstPersonAIO>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pre_pee)
        {
            Pee_curTimeTillGameOver += Time.deltaTime;
            slider.value = Pee_curTimeTillGameOver;
            if(Pee_curTimeTillGameOver > Pee_maxTimeTillGameOver)
            {
                //GAME OVER 
                if(gameOverUI!=null)
                {
                    gameOverUI.activeFade();
                }
                else
                {
                    loadAnyScene("Menu");
                }

            }
        }
        else
        {
            if(Pee_curTimeTillGameOver > 0)
            {
                Pee_curTimeTillGameOver -= Time.deltaTime *peeDecreaseSpeed;
                slider.value = Pee_curTimeTillGameOver;
            }
            else
            {
                Player.GetComponent<FirstPersonAIO>().playerCanMove = true;

                sliderFill.sprite = anxityMeter;
                sliderBackground.sprite = anxityBackground;
                slider.maxValue = anxity_maxTimeTillGameOver;
                //sliderFill.color = anxityColour;
                anixty_curTimeTillGameOver += Time.deltaTime;
                slider.value = anixty_curTimeTillGameOver;
                if (anixty_curTimeTillGameOver > anxity_maxTimeTillGameOver)
                {
                    //GAME OVER 
                    if (gameOverUI != null)
                    {
                        gameOverUI.activeFade();
                    }
                    else
                    {
                        loadAnyScene("Menu");
                    }

                }
            }
        }
    }

    public void depletePissMeter()
    {
        StartCoroutine("depletePiss");
    }

    //this doesn't work currently look into it 
    Coroutine depletePiss()
    {
        do
        {
            Pee_curTimeTillGameOver -= Time.deltaTime;
            slider.value = Pee_curTimeTillGameOver;

            return null;
        } while (slider.value > 0);


         
    }

    public void loadAnyScene(string level)
    {
        SceneManager.LoadScene(level);
        Cursor.lockState = CursorLockMode.None;
    }


    public void activeLossScreen()
    {
        //GAME OVER 
        if (gameOverUI != null)
        {
            gameOverUI.activeFade();
        }
        else
        {
            loadAnyScene("Menu");

        }
    }

    public void activateWinScreen()
    {
        if (winUI != null)
        {
            winUI.activeFade();
        }
        else
        {
            loadAnyScene("Menu");
        }
    }



    public void lockMovement()
    {
        firstPerson.playerCanMove = false;
        Debug.Log("are lock movemenet?? sequence");
    }

    public void unlockMovement()
    {
        firstPerson.playerCanMove = true;
        Debug.Log("are unlock movemenet??sequence");
    }
}
