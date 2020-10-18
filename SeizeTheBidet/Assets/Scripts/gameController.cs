using System.Collections;
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
    public bool peeFinished;
    float peeSoundLenght = 20;
    float peeSoundTimeRemaining;

    public GameObject Player;
    FirstPersonAIO firstPerson;

    public bool winable;

    public bool cutsceneFinished;
    public DeathScreenFade fadeToBlackCutscene;

    public PuzzleController currentPuzzle;
    GameObject ghostBros;

    public AudioClip postPeeSong;

    public AudioClip victorySound1, victorySound2;
    public AudioClip gameOverSound;
    AudioSource audio;
    bool playOnce;
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
        playOnce = false;
        Pee_curTimeTillGameOver = 0;
        peeSoundTimeRemaining = peeSoundLenght;
        slider.maxValue = Pee_maxTimeTillGameOver;
        audio = GetComponent<AudioSource>();
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
            if(Pee_curTimeTillGameOver > 0 || peeSoundTimeRemaining > 0)
            {
                Pee_curTimeTillGameOver -= Time.deltaTime *peeDecreaseSpeed;
                peeSoundTimeRemaining -= Time.deltaTime;


                slider.value = Pee_curTimeTillGameOver;
                fadeToBlackCutscene.noSceneChange=true;
                fadeToBlackCutscene.activeFade();
                if ((Pee_curTimeTillGameOver <= 0 && peeSoundTimeRemaining <=0))
                {
                    Debug.Log("begin the FADEback");
                    fadeToBlackCutscene.autoFadeback = true;
                    fadeToBlackCutscene.fade = false;
                    //fadeToBlackCutscene.activeFade();
                }
            }
            else if(cutsceneFinished)
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
                    activeLossScreen();

                }
            }
        }
    }

    public void depletePissMeter(GameObject bros)
    {
        ghostBros = bros;
    }



    public void loadAnyScene(string level)
    {
        SceneManager.LoadScene(level);
        Cursor.lockState = CursorLockMode.None;
    }


    public void activeLossScreen()
    {
        //GAME OVER 
        if (!playOnce)
        {
            audio.Stop();
            audio.clip = gameOverSound;
            audio.volume = 0.4f;
            audio.Play();
            playOnce = true;
        }
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
        if (!playOnce)
        {
            audio.Stop();
            if (Random.Range(0, 1f) > 0.5f)
                audio.clip = victorySound1;
            else
                audio.clip = victorySound2;
            audio.volume = 0.4f;
            audio.Play();
            playOnce = true;
        }
        if (winUI != null)
        {
            winUI.activeFade();
        }
        else
        {
            loadAnyScene("Menu");
        }
    }

    public void activateBroDudes()
    {

        // something about dudes 
        ghostBros.SetActive(true);
        //
        audio.clip = postPeeSong;
        audio.Play();
    }

    public void broGhostCutsceneFinished()
    {
        cutsceneFinished = true;
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


    public void unblockDoors()
    {
        if(currentPuzzle!=null)
        {
            currentPuzzle.BlockRoom(false);
        }
    }
}
