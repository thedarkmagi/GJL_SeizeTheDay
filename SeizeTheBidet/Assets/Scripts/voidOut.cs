using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voidOut : MonoBehaviour
{

    public GameObject player;
    public Transform voidOutPlane;
    public float Ymodifier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y < voidOutPlane.position.y + Ymodifier)
        {
            gameController.instance.gameOverUI.activeFade();
        }
    }
}
