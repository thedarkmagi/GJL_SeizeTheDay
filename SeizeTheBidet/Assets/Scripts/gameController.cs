using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameController : MonoBehaviour
{
    public static gameController instance;

    public bool pre_pee=true;


    public float Pee_maxTimeTillGameOver;
    public Slider slider;
    float Pee_curTimeTillGameOver;

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
            }
        }
    }
}
