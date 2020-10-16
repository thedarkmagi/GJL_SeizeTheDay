using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashSkeley : MonoBehaviour
{
    public puzzlePieces puzzlePieces;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (puzzlePieces != null)
            {
                //room.closeDoors();
                puzzlePieces.complete = true;
                //this disapears a skeleyton
                var rends = GetComponentsInChildren<Renderer>();
                foreach (var item in rends)
                {
                    item.enabled = false;
                }
            }
            //meshRenderer.material = unlockedRoomMat;
        }
    }
}
