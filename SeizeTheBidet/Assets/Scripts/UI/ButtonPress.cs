﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadAnyScene(string level)
    {
        SceneManager.LoadScene(level);
    }


    public void quit()
    {
        Application.Quit();
    }
}
