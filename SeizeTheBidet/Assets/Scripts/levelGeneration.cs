using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum roomType
{
    notSet,
    empty,
    entry,
    puzzleRoom,
    bathroom,
    voidRoom,
}



public class levelGeneration : MonoBehaviour
{
    public GameObject Player;
    [Range(0,1)]
    public float additionPathBranchGeneration = 0.7f;
    public int map_x, map_y;
    public float x_roomSize, y_roomSize;
    GameObject[,] level;
    roomType[,] roomTypes;

    int[,] moveCost;
    public GameObject room;
    public List<GameObject> puzzleRooms;
    public GameObject entryRoom;
    public GameObject EndRoom;
    public GameObject voidRoom;

    int entryX;

    List<Vector2Int> directions;

    List<Vector2Int> findTheExitPath = new List<Vector2Int>();


    public bool post_pee;
    // Start is called before the first frame update
    void Start()
    {
        directions = new List<Vector2Int>();
        directions.Add(new Vector2Int(-1, 0));
        directions.Add(new Vector2Int(1, 0));
        directions.Add(new Vector2Int(0, -1));
        directions.Add(new Vector2Int(0, 1));


        level = new GameObject[map_x, map_y];
        roomTypes = new roomType[map_x, map_y];
        moveCost = new int[map_x, map_y];
        entryX = 0;
        defineRooms();
        generateGrid();

        if(Player!=null)
        {
            Player.transform.position = level[entryX, 0].GetComponent<StartRoomPos>().getSpawnPos().position;
            //Player.transform.position = level[entryX, 0].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(post_pee)
        {
            //postPee();
        }
        
    }

    public void postPee()
    {
        for (int m_x = 0; m_x < map_x; m_x++)
        {
            for (int m_y = 0; m_y < map_y; m_y++)
            {
                if(roomTypes[m_x, m_y] == roomType.puzzleRoom)
                {
                    //not actually desired behavour was purely as a test
                    //level[m_x, m_y].GetComponent<RoomScript>().lockDoors();
                }
            }
        }

        //post_pee = false;
    }

    public void defineRooms()
    {
        for (int m_x = 0; m_x < map_x; m_x++)
        {
            for (int m_y = 0; m_y < map_y; m_y++)
            {
                roomTypes[m_x, m_y] = roomType.notSet;
            }
        }

        entryX =(int) Random.Range(0, map_x-1);
        Debug.Log(entryX);
        roomTypes[entryX, 0] = roomType.entry;

        for (int m_x = 0; m_x < map_x; m_x++)
        {
            for (int m_y = 0; m_y < map_y; m_y++)
            {
                //Vector3 pos = new Vector3(m_x * x_roomSize, 0, m_y * y_roomSize);
                if (roomTypes[m_x, m_y] == roomType.notSet)
                {
                    var temp = roomType.empty;
                    if (Random.Range(0,1.0f) > 0.5f)
                    {
                        temp = roomType.puzzleRoom;
                    }
                    roomTypes[m_x, m_y] = temp;
                }

            }
        }

        var endRoom = findEndRoom();
        if(endRoom.x != int.MinValue)
        {

            roomTypes[endRoom.x, endRoom.y] = roomType.bathroom;
            //this might have been a terrible approach going to try something else
            //ensureYouCanGetToEnd(endRoom);
            aStarApproach(endRoom);
            addVoidRooms();
        }
    }

    public void aStarApproach(Vector2Int endRoom)
    {
        for (int m_x = 0; m_x < map_x; m_x++)
        {
            for (int m_y = 0; m_y < map_y; m_y++)
            {
                moveCost[m_x,m_y] = int.MaxValue;
            }
        }

        moveCost[endRoom.x, endRoom.y] = 0;
        List<Vector2Int> remainingChecks = new List<Vector2Int>();
        remainingChecks.Add(endRoom);
        do
        {
            if (remainingChecks.Count > 0)
            {
                remainingChecks.AddRange(asignCost(remainingChecks[0]));
                remainingChecks.RemoveAt(0);
            }
        } while (remainingChecks.Count > 0);

        //for (int m_x = 0; m_x < map_x; m_x++)
        //{
        //    for (int m_y = 0; m_y < map_y; m_y++)
        //    {
        //        Debug.Log("POS:"+ m_x + "/" + m_y + " Cost " + moveCost[m_x, m_y]);
        //    }
        //}
        //cost is set now define a path I guess?
        //this is what mistakes look like

        List<Vector2Int> currentPos = new List<Vector2Int>();

        //Vector2Int currentPos = new Vector2Int(entryX, 0);
        currentPos.Add(new Vector2Int(entryX, 0));
        findTheExitPath.Add(currentPos[0]);
        int i = 0;
        while(true)
        {
            for (int c = 0; c < currentPos.Count; c++)
            {
                currentPos[c] = findCheapest(currentPos[c]);
                findTheExitPath.Add(currentPos[0]);

                if (Random.Range(0, 1.0f) < additionPathBranchGeneration)
                {
                    var newBranch = find2ndCheapest(currentPos[c]);
                    currentPos.Add(newBranch);
                    //currentPos 



                    findTheExitPath.Add(newBranch);
                }


                if (moveCost[currentPos[c].x, currentPos[c].y] == 0)
                {
                    Debug.Log("found path to exit");
                    i = 1000;
                    break;
                }
            }
            //currentPos[0] = findCheapest(currentPos[0]);
            //findTheExitPath.Add(currentPos[0]);

            //if (moveCost[currentPos.x,currentPos.y] == 0)
            //{
            //    Debug.Log("found path to exit");
            //    break;
            //}

            i++;
            if(i > 100)
            {
                Debug.Log("forceBreak out");
                break;
            }
        }
    }

    public List<Vector2Int> asignCost(Vector2Int currentRoom)
    {
        List<Vector2Int> result = new List<Vector2Int>();

        for (int i = 0; i < directions.Count; i++)
        {
            int m_x = currentRoom.x + directions[i].x;
            int m_y = currentRoom.y + directions[i].y;


            if (checkInsideBounds(m_x, m_y))
            {
                if (moveCost[m_x, m_y] == int.MaxValue)
                {
                    int additionCost = 1;
                    if(roomTypes[m_x, m_y] == roomType.empty)
                    {
                        additionCost = 5;
                        //roomTypes[m_x, m_y] = roomType.puzzleRoom;
                    }

                    moveCost[m_x, m_y] = moveCost[currentRoom.x, currentRoom.y] + additionCost;
                    result.Add(new Vector2Int(m_x, m_y));
                }
            }
        }

        return result;
    }

    public Vector2Int findCheapest( Vector2Int current)
    {
        Vector2Int cheapest = new Vector2Int();
        int currentCost = int.MaxValue;

        for (int i = 0; i < directions.Count; i++)
        {
            int m_x = current.x + directions[i].x;
            int m_y = current.y + directions[i].y;


            if (checkInsideBounds(m_x, m_y))
            {
                if(moveCost[m_x,m_y] < currentCost)
                {
                    currentCost = moveCost[m_x, m_y];
                    cheapest = new Vector2Int(m_x, m_y);
                }
            }
        }

        if (roomTypes[cheapest.x, cheapest.y] == roomType.empty)
        {
            roomTypes[cheapest.x, cheapest.y] = roomType.puzzleRoom;
        }

        return cheapest;

    }
    public Vector2Int find2ndCheapest(Vector2Int current)
    {
        Vector2Int cheapest = new Vector2Int();
        int currentCost = int.MaxValue;
        int minCost = int.MaxValue;

        for (int i = 0; i < directions.Count; i++)
        {
            int m_x = current.x + directions[i].x;
            int m_y = current.y + directions[i].y;


            if (checkInsideBounds(m_x, m_y))
            {
                if (moveCost[m_x, m_y] < minCost)
                {
                    minCost = moveCost[m_x, m_y];
                    //cheapest = new Vector2Int(m_x, m_y);
                }
            }
        }

        for (int i = 0; i < directions.Count; i++)
        {
            int m_x = current.x + directions[i].x;
            int m_y = current.y + directions[i].y;


            if (checkInsideBounds(m_x, m_y))
            {
                if (moveCost[m_x, m_y] < currentCost && moveCost[m_x, m_y] > minCost)
                {
                    currentCost = moveCost[m_x, m_y];
                    cheapest = new Vector2Int(m_x, m_y);
                }
            }
        }

        if (roomTypes[cheapest.x, cheapest.y] == roomType.empty)
        {
            roomTypes[cheapest.x, cheapest.y] = roomType.puzzleRoom;
        }

        return cheapest;

    }

    public void ensureYouCanGetToEnd(Vector2Int endRoom)
    {
        int x = entryX, y = 0;
        bool pathComplete = false;
        int loops = 0;

        while(!pathComplete)
        {
            var nextRooms = checkForPath(x,y);
            int closestOption = 0;
            Vector2Int closetRoom = new Vector2Int(int.MaxValue, int.MaxValue);
            if(nextRooms.Count>0)
            {
                for (int i = 0; i < nextRooms.Count; i++)
                {
                    if (i == 0)
                    {
                        closestOption = i;
                        closetRoom.x = nextRooms[i].x;
                        closetRoom.y = nextRooms[i].y;
                        Debug.Log("setFirstRoom");
                    }
                    else
                    {
                        if (Mathf.Abs(nextRooms[i].x - endRoom.x) < Mathf.Abs(closetRoom.x - endRoom.x) || Mathf.Abs(nextRooms[i].y - endRoom.y) < Mathf.Abs(closetRoom.y - endRoom.y))
                        {
                            closestOption = i;
                            closetRoom.x = nextRooms[i].x;
                            closetRoom.y = nextRooms[i].y;
                            Debug.Log("found closer room");
                        }
                    }
                }
                
            }
            else
            {
                //int xMod = 1;
                //if(endRoom.x - x+1 < endRoom.x - (x - 1))
                //{
                //    //closer? to the exit I think
                //    xMod = 1;
                //}
                //else
                //{
                //    xMod = -1;
                //}
                //xMod += x;
                //roomTypes[xMod, y]= roomType.puzzleRoom;
                //closetRoom.x = xMod;
                //closetRoom.y = y;
                //Debug.Log("Force Spawn Puzzle Room: " + xMod + ":" + y);

                int newX = 0, newY = 0;
                if (Mathf.Abs(x - 1 - endRoom.x) < Mathf.Abs(x + 1 - endRoom.x))
                {
                    newX = x - 1;
                }
                else
                {
                    newX = x + 1;
                }
                closetRoom.x = newX;
                closetRoom.y = y;
                if (checkInsideBounds(closetRoom.x, closetRoom.y))
                {
                    roomTypes[closetRoom.x, y] = roomType.puzzleRoom;
                    Debug.Log("force new room when the only option is back X");
                }
                else
                {
                    if (Mathf.Abs(y - 1 - endRoom.y) < Mathf.Abs(y + 1 - endRoom.y))
                    {
                        newY = y - 1;
                    }
                    else
                    {
                        newY = y + 1;
                    }
                    closetRoom.x = x;
                    closetRoom.y = newY;
                    if (checkInsideBounds(closetRoom.x, closetRoom.y))
                    {
                        roomTypes[closetRoom.x, closetRoom.y] = roomType.puzzleRoom;
                        Debug.Log("force new room when the only option is back Y");
                    }
                    else
                    {
                        Debug.Log("fuck apparently no direction is legal???");
                    }
                }
            }

            if (findTheExitPath.Count > 0)
            {
                if (findTheExitPath.Contains(closetRoom))
                {
                    int newX = 0, newY = 0;
                    if (Mathf.Abs(x - 1 - endRoom.x) < Mathf.Abs(x + 1 - endRoom.x))
                    {
                        newX = x - 1;
                    }
                    else
                    {
                        newX = x + 1;
                    }
                    closetRoom.x = newX;
                    closetRoom.y = y;
                    if (checkInsideBounds(closetRoom.x, closetRoom.y))
                    {
                        roomTypes[closetRoom.x, y] = roomType.puzzleRoom;
                        Debug.Log("force new room when the only option is back X");
                    }
                    else
                    {
                        if (Mathf.Abs(y - 1 - endRoom.y) < Mathf.Abs(y + 1 - endRoom.y))
                        {
                            newY = y - 1;
                        }
                        else
                        {
                            newY = y + 1;
                        }
                        closetRoom.x = x;
                        closetRoom.y = newY;
                        if (checkInsideBounds(closetRoom.x, closetRoom.y))
                        {
                            roomTypes[closetRoom.x, closetRoom.y] = roomType.puzzleRoom;
                            Debug.Log("force new room when the only option is back Y");
                        }
                        else
                        {
                            Debug.Log("fuck apparently no direction is legal???");
                        }
                    }
                }
            }


            x = closetRoom.x;
            y = closetRoom.y;
            Debug.Log("we have do add path: " + x + ":" + y);
            findTheExitPath.Add(new Vector2Int(x, y));
            if (x == endRoom.x && y == endRoom.y)
            {
                pathComplete = true;
            }
            loops += 1;
            if( loops > 40)
            {
                Debug.Log("BROKE OUT, full timeout on finding a path");
                break;
            }
        }
    }

    public List<Vector2Int> checkForPath(int x,int y)
    {
        List<Vector2Int> result = new List<Vector2Int>();

        for (int i = 0; i < directions.Count; i++)
        {
            int m_x = x + directions[i].x;
            int m_y = y + directions[i].y;


            if (checkInsideBounds(m_x, m_y))
            {
                if (roomTypes[m_x, m_y] != roomType.empty && roomTypes[m_x, m_y] != roomType.notSet)
                {
                    result.Add(new Vector2Int(m_x, m_y));
                }
            }
        }

        return result;
    }

    public bool checkInsideBounds(int x,int y)
    {
        bool result = true;
        

        if (x>=map_x  || y>=map_y || x<0 || y<0)
        {
            //Debug.Log("NOT insideBounds:" + x + ">" + map_x + " OR " + y + ">" + map_y);
            result = false;
        }


        return result;
    }


    public Vector2Int findEndRoom()
    {
        Vector2Int result = new Vector2Int(0,0);

        List<Vector2Int> emptyRooms = new List<Vector2Int>();

        for (int m_x = 0; m_x < map_x; m_x++)
        {
            for (int m_y = 0; m_y < map_y; m_y++)
            {
                if (roomTypes[m_x, m_y] == roomType.empty)
                {
                    emptyRooms.Add(new Vector2Int(m_x, m_y));
                }
            }
        }

        int xDist = int.MinValue;
        int yDist = int.MinValue;

        for (int i = 0; i < emptyRooms.Count; i++)
        {
            if(emptyRooms[i].x >= xDist && emptyRooms[i].y >= yDist)
            {
                xDist = (int)emptyRooms[i].x;
                yDist = (int)emptyRooms[i].y;
            }
        }
        result.x = xDist;
        result.y = yDist;

        return result;
    }


    public void addVoidRooms()
    {
        for (int m_x = 0; m_x < map_x; m_x++)
        {
            for (int m_y = 0; m_y < map_y; m_y++)
            {
                Vector3 pos = new Vector3(m_x * x_roomSize, 0, m_y * y_roomSize);

                switch (roomTypes[m_x, m_y])
                {
                    case roomType.notSet:
                        roomTypes[m_x, m_y] = roomType.voidRoom;
                        break;
                    case roomType.empty:
                        roomTypes[m_x, m_y] = roomType.voidRoom;
                        break;
                        break;
                    default:
                        break;
                }


            }
        }
    }

    public void generateGrid()
    {
        for (int m_x = 0; m_x < map_x; m_x++)
        {
            for (int m_y = 0; m_y < map_y; m_y++)
            {
                Vector3 pos = new Vector3(m_x * x_roomSize, 0, m_y * y_roomSize);

                switch (roomTypes[m_x,m_y])
                {
                    case roomType.notSet:
                        break;
                    case roomType.empty:
                        break;
                    case roomType.entry:
                        var m_entry = Instantiate(entryRoom, pos, Quaternion.identity);
                        level[m_x, m_y] = m_entry;
                        level[m_x, m_y].transform.parent = this.transform;
                        break;
                    case roomType.puzzleRoom:
                        int randRoom = Random.Range(0, puzzleRooms.Count);
                        var m_room = Instantiate(puzzleRooms[randRoom], pos, Quaternion.identity);
                        level[m_x, m_y] = m_room;
                        level[m_x, m_y].transform.parent = this.transform;
                        break;
                    case roomType.bathroom:
                        var m_roomEnd = Instantiate(EndRoom, pos, Quaternion.identity);
                        level[m_x, m_y] = m_roomEnd;
                        level[m_x, m_y].transform.parent = this.transform;
                        break;
                    case roomType.voidRoom:
                        var m_voidRoom = Instantiate(voidRoom, pos, Quaternion.identity);
                        level[m_x, m_y] = m_voidRoom;
                        level[m_x, m_y].transform.parent = this.transform;
                        break;
                    default:
                        break;
                }


            }
        }

        // adds an external set of rooms from the array that will be all void rooms 
        //to ensure no falling out of the map 
        //as this only does things on the boarder of the array effectively
        for (int x = -1; x < map_x+1; x++)
        {
            for (int y = -1; y < map_y+1; y++)
            {
                if(x==-1 || y == -1 || y == map_y || x == map_x)
                {
                    Vector3 pos = new Vector3(x * x_roomSize, 0, y * y_roomSize);
                    var m_voidRoom = Instantiate(voidRoom, pos, Quaternion.identity);
                    m_voidRoom.transform.parent = this.transform;
                    
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        for (int i = 0; i < findTheExitPath.Count; i++)
        {
            Gizmos.color = Color.blue;
            Vector3 startpos = new Vector3(findTheExitPath[i].x * x_roomSize, 15, findTheExitPath[i].y * y_roomSize);
            Vector3 endPos;


            if (i==0)
            {
                endPos = startpos;
            }
            else
            {
                endPos = new Vector3(findTheExitPath[i-1].x * x_roomSize, 15, findTheExitPath[i-1].y * y_roomSize);
            }

            

            Gizmos.DrawLine(startpos, endPos);
        }
    }
}
