using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public RoomScript room;
    public List<puzzlePieces> pre_puzzlePieces;
    public List<GameObject> pre_partsOfPuzzle;
    
    
    public List<puzzlePieces> post_puzzlePieces;
    public List<GameObject> post_partsOfPuzzle;
    public List<GameObject> post_DoorBlocks;

    public bool pre_peeChallenge;
    public bool post_peeChallenge;

    public bool postSolved;

    bool localPre_pee;
    // Start is called before the first frame update
    void Start()
    {
        localPre_pee = gameController.instance.pre_pee;

        setupRoom();

        if(!pre_peeChallenge)
        {
            if (room != null)
                room.openDoors = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(localPre_pee != gameController.instance.pre_pee)
        {
            changeOfState();
        }


        if (gameController.instance.pre_pee)
        {
            if (pre_peeChallenge)
            {
                if (checkIfSolved(pre_puzzlePieces))
                {
                    if (room != null)
                        room.openDoors = true;
                    FindObjectOfType<MeleeAttack>().disableEquipment();
                    pre_peeChallenge = false;
                }
            }
        }
        else
        {
            if (post_peeChallenge)
            {
                if (checkIfSolved(post_puzzlePieces))
                {
                    if (room != null)
                        room.openDoors = true;
                    FindObjectOfType<MeleeAttack>().disableEquipment();
                    post_peeChallenge = false;
                    postSolved = true;
                    BlockRoom(false);
                    // clear matches or something 
                    for (int i = 0; i < post_partsOfPuzzle.Count; i++)
                    {
                        post_partsOfPuzzle[i].SetActive(false);
                    }
                }
            }
        }


        localPre_pee = gameController.instance.pre_pee;

    }

    public void changeOfState()
    {
        //disable anything that needs to change 

        if (post_peeChallenge)
        {
            if (pre_peeChallenge)
            {
                for (int i = 0; i < pre_puzzlePieces.Count; i++)
                {
                    pre_puzzlePieces[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < pre_partsOfPuzzle.Count; i++)
                {
                    pre_partsOfPuzzle[i].SetActive(false);
                }
            }
            //enable new puzzle if needed
            for (int i = 0; i < post_puzzlePieces.Count; i++)
            {
                post_puzzlePieces[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < post_partsOfPuzzle.Count; i++)
            {
                post_partsOfPuzzle[i].SetActive(true);
            }
            if (room != null)
            {
                //room.openDoors = false;
                //room.lockDoors();
            }
        }
    }


    public void setupRoom()
    {

        if (pre_peeChallenge)
        {
            for (int i = 0; i < pre_puzzlePieces.Count; i++)
            {
                pre_puzzlePieces[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < pre_partsOfPuzzle.Count; i++)
            {
                pre_partsOfPuzzle[i].SetActive(true);
            }  
        }
        if (post_peeChallenge)
        {
            for (int i = 0; i < post_puzzlePieces.Count; i++)
            {
                post_puzzlePieces[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < post_partsOfPuzzle.Count; i++)
            {
                post_partsOfPuzzle[i].SetActive(false);
            }
            BlockRoom(false);
        }


    }
    
    

    bool checkIfSolved(List<puzzlePieces> puzzlePieces)
    {
        bool solved = true;
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            if(puzzlePieces[i].complete!=true)
            {
                solved = false;
                break;
            }
        }

        return solved;
    }


    public void BlockRoom(bool active)
    {
        for (int i = 0; i < post_DoorBlocks.Count; i++)
        {
            post_DoorBlocks[i].SetActive(active);
        }
    }
}
