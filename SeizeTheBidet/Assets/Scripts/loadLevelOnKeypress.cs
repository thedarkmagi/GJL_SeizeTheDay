﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loadLevelOnKeypress : MonoBehaviour
{


    public string levelName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            loadAnyScene(levelName);
        }
    }


    public void loadAnyScene(string level)
    {
        SceneManager.LoadScene(level);
        Cursor.lockState = CursorLockMode.None;
    }
}
