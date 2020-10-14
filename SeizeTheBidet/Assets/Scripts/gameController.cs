using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public static gameController instance;

    public bool pre_pee=true;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
