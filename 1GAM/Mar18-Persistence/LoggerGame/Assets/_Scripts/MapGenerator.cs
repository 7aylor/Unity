using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public int sizeX;
    public int sizeY;
    //public int[,] map;
    public Transform terrainContainer;

    [Range(0,5)]
    public int smoothCycles;

    [Range(0, 100)]
    public int treeSpawnThreshold;

    [Range(0, 100)]
    public int rockSpawnThreshold;

    [Range(0,6)]
    public int neighborThreshold;

    #region GameObjects
    public GameObject grass;
    public GameObject[] trees;
    public GameObject rock;
    public GameObject riverStraight;
    public GameObject riverCurve;
    public GameObject riverStart;
    public GameObject riverEnd;
    #endregion

    private enum tileType
    {
        grass,
        tree,
        rock,
        startRiver,
        straightRiver,
        curveRiver,
        endRiver
    }

    private void Awake()
    {
        
    }

    private void Start()
    {
        GameManager.instance.InstantiateMap(sizeX, sizeY);
        GenerateRandomMap();
        SmoothMap();
        CreateRiver();
        SpawnTerrain();
    }

    private void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            DestroyTerrain();
            GenerateRandomMap();
            SmoothMap();
            CreateRiver();
            SpawnTerrain();
        }
    }

    private void GenerateRandomMap()
    {
        for(int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                int num = Random.Range(0, 100);

                if (num < treeSpawnThreshold)
                {
                    GameManager.instance.map[x, y] = (int)tileType.tree;
                }
                else if (num > rockSpawnThreshold)
                {
                    GameManager.instance.map[x, y] = (int)tileType.rock;
                }
                else
                {
                    GameManager.instance.map[x, y] = (int)tileType.grass;
                }
            }
        }
    }

    private void CreateRiver()
    {
        bool startOnX = System.Convert.ToBoolean(Random.Range(0, 2));

        int startX = 0;
        int startY = 0;

        //start on x axis x=0 or x=sizeX
        if (startOnX == true)
        {
            startX = PickSide(sizeX);
            startY = Random.Range(1, sizeY - 2);

            int x = startX;
            int y = startY;

            //if the river starts on the right side of the screen
            if(x > 0)
            {
                while(x > 0 && y > 0 && y < sizeY - 1)
                {
                    LayRiverTilesStartOnRight(ref x, ref y);
                }
            }
            //if the river starts on the left side of the screen
            else
            {
                while (x < sizeX - 1 && y > 0 && y < sizeY - 1)
                {
                    LayRiverTilesStartOnLeft(ref x, ref y);
                }
            }
        }
        //start on y axis
        else
        {
            startY = PickSide(sizeY);
            startX = Random.Range(1, sizeX - 2);

            int x = startX;
            int y = startY;

            //on the top
            if (startY > 0)
            {
                while (y > 0 && x > 0 && x < sizeX - 1)
                {
                    LayRiverTilesStartOnTop(ref x, ref y);
                }
            }
            //on top
            else
            {
                while (y < sizeY - 1 && x > 0 && x < sizeX - 1)
                {
                    LayRiverTilesStartOnBot(ref x, ref y);
                }
            }
        }

        GameManager.instance.map[startX, startY] = (int)tileType.startRiver;
    }

    private void LayRiverTilesStartOnRight(ref int x, ref int y)
    {
        int randomDistance = Random.Range(2, 4);
        int steps = 0;

        int randDir = GetRiverDirection(x, y);

        while (steps < randomDistance && x >= 0 && (y >= 0 && y <= sizeY - 1))
        {
            steps++;

            switch (randDir)
            {
                //up
                case 0:
                    if(isRiverTile(x, y + 1) || x == sizeX - 1)
                    {
                        return;
                    }

                    y++;

                    GameManager.instance.map[x, y] = (int)tileType.straightRiver;
                    break;
                //down
                case 1:
                    if (isRiverTile(x, y - 1) || x == sizeX - 1)
                    {
                        return;
                    }

                    y--;

                    GameManager.instance.map[x, y] = (int)tileType.straightRiver;
                    break;
                //left
                case 2:
                    if (isRiverTile(x - 1, y))
                    {
                        return;
                    }

                    x--;

                    GameManager.instance.map[x, y] = (int)tileType.straightRiver;
                    break;
            }

            //check if we have hit the end
            if (x <= 0)
            {
                Debug.Log("Hit left edge, created endRiver tile");
                GameManager.instance.map[x, y] = (int)tileType.endRiver;
                return;
            }
            if (y <= 0)
            {
                Debug.Log("Hit top edge, created endRiver tile");
                GameManager.instance.map[x, y] = (int)tileType.endRiver;
                return;
            }
            if (y >= sizeY - 1)
            {
                Debug.Log("Hit bottom edge, created endRiver tile");
                GameManager.instance.map[x, y] = (int)tileType.endRiver;
                return;
            }

        }
    }

    private void LayRiverTilesStartOnLeft(ref int x, ref int y)
    {
        int randomDistance = Random.Range(2, 4);
        int steps = 0;

        int randDir = GetRiverDirection(x, y);

        while (steps < randomDistance && x < sizeX - 1 && (y >= 0 && y <= sizeY - 1))
        {
            steps++;

            switch (randDir)
            {
                //up
                case 0:
                    if (isRiverTile(x, y + 1) || x == 0)
                    {
                        return;
                    }

                    y++;

                    GameManager.instance.map[x, y] = (int)tileType.straightRiver;
                    break;
                //down
                case 1:
                    if (isRiverTile(x, y - 1) || x == 0)
                    {
                        return;
                    }

                    y--;

                    GameManager.instance.map[x, y] = (int)tileType.straightRiver;
                    break;
                //right
                case 2:
                    if (isRiverTile(x + 1, y))
                    {
                        return;
                    }

                    x++;

                    GameManager.instance.map[x, y] = (int)tileType.straightRiver;
                    break;
            }

            //check if we have hit the end
            if (x >= sizeX - 1)
            {
                Debug.Log("Hit left edge, created endRiver tile");
                GameManager.instance.map[x, y] = (int)tileType.endRiver;
                return;
            }
            if (y <= 0)
            {
                Debug.Log("Hit top edge, created endRiver tile");
                GameManager.instance.map[x, y] = (int)tileType.endRiver;
                return;
            }
            if (y >= sizeY - 1)
            {
                Debug.Log("Hit bottom edge, created endRiver tile");
                GameManager.instance.map[x, y] = (int)tileType.endRiver;
                return;
            }

        }
    }

    private void LayRiverTilesStartOnTop(ref int x, ref int y)
    {
        int randomDistance = Random.Range(2, 4);
        int steps = 0;

        int randDir = GetRiverDirection(x, y);

        while (steps < randomDistance && y > 0 && (x >= 0 && x <= sizeX - 1))
        {
            steps++;

            switch (randDir)
            {
                //down
                case 0:
                    if (isRiverTile(x, y - 1))
                    {
                        return;
                    }

                    y--;

                    GameManager.instance.map[x, y] = (int)tileType.straightRiver;
                    break;
                //left
                case 1:
                    if (isRiverTile(x - 1, y) || y == sizeY - 1)
                    {
                        return;
                    }

                    x--;

                    GameManager.instance.map[x, y] = (int)tileType.straightRiver;
                    break;
                //right
                case 2:
                    if (isRiverTile(x + 1, y) || y == sizeY - 1)
                    {
                        return;
                    }

                    x++;

                    GameManager.instance.map[x, y] = (int)tileType.straightRiver;
                    break;
            }

            //check if we have hit the end
            if (x >= sizeX - 1)
            {
                Debug.Log("Hit left edge, created endRiver tile");
                GameManager.instance.map[x, y] = (int)tileType.endRiver;
                return;
            }
            if (x <= 0)
            {
                Debug.Log("Hit top edge, created endRiver tile");
                GameManager.instance.map[x, y] = (int)tileType.endRiver;
                return;
            }
            if (y <= 0)
            {
                Debug.Log("Hit bottom edge, created endRiver tile");
                GameManager.instance.map[x, y] = (int)tileType.endRiver;
                return;
            }

        }
    }

    private void LayRiverTilesStartOnBot(ref int x, ref int y)
    {
        int randomDistance = Random.Range(2, 4);
        int steps = 0;

        int randDir = GetRiverDirection(x, y);

        while (steps < randomDistance && y < sizeY - 1 && (x >= 0 && x <= sizeX - 1))
        {
            steps++;

            switch (randDir)
            {
                //down
                case 0:
                    if (isRiverTile(x, y + 1))
                    {
                        return;
                    }

                    y++;

                    GameManager.instance.map[x, y] = (int)tileType.straightRiver;
                    break;
                //left
                case 1:
                    if (isRiverTile(x - 1, y) || y == 0)
                    {
                        return;
                    }

                    x--;

                    GameManager.instance.map[x, y] = (int)tileType.straightRiver;
                    break;
                //right
                case 2:
                    if (isRiverTile(x + 1, y) || y == 0)
                    {
                        return;
                    }

                    x++;

                    GameManager.instance.map[x, y] = (int)tileType.straightRiver;
                    break;
            }

            //check if we have hit the end
            if (x >= sizeX - 1)
            {
                Debug.Log("Hit left edge, created endRiver tile");
                GameManager.instance.map[x, y] = (int)tileType.endRiver;
                return;
            }
            if (x <= 0)
            {
                Debug.Log("Hit top edge, created endRiver tile");
                GameManager.instance.map[x, y] = (int)tileType.endRiver;
                return;
            }
            if (y >= sizeY - 1)
            {
                Debug.Log("Hit bottom edge, created endRiver tile");
                GameManager.instance.map[x, y] = (int)tileType.endRiver;
                return;
            }

        }
    }

    /// <summary>
    /// Use this method to determine if the direction the river is going is going into the direction of what is already a river
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private bool isRiverTile(int x, int y)
    {
        if (GameManager.instance.map[x, y] == (int)tileType.curveRiver || 
            GameManager.instance.map[x, y] == (int)tileType.endRiver || 
            GameManager.instance.map[x, y] == (int)tileType.startRiver || 
            GameManager.instance.map[x, y] == (int)tileType.straightRiver)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Used to check which directions the next river tile can go in
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private int GetRiverDirection(int x, int y)
    {

        int acceptableNeighborCount = 4;

        List<int> riverTypes = new List<int>{ (int)tileType.curveRiver,
                                    (int)tileType.endRiver, (int)tileType.startRiver, (int)tileType.straightRiver};

        if (x > 0 && x < sizeX - 1 && y > 0 && y < sizeY - 1)
        {
            //left
            if (riverTypes.Contains(GameManager.instance.map[x - 1, y]))
            {
                acceptableNeighborCount--;
            }
            //right 1
            if (riverTypes.Contains(GameManager.instance.map[x + 1, y]))
            {
                acceptableNeighborCount--;
            }
            //up
            if (riverTypes.Contains(GameManager.instance.map[x, y + 1]))
            {
                acceptableNeighborCount--;
            }
            //down
            if (riverTypes.Contains(GameManager.instance.map[x, y - 1]))
            {
                acceptableNeighborCount--;
            }
        }

        return Random.Range(0, acceptableNeighborCount);
    }

    /// <summary>
    /// Picks the side of the screen to start on when creating the river
    /// </summary>
    /// <param name="maxSize"></param>
    /// <returns></returns>
    private int PickSide(int maxSize)
    {
        int startVal;
        if (Random.Range(0, 2) == 0)
        {
            startVal = 0;
        }
        else
        {
            startVal = maxSize - 1;
        }

        return startVal;
    }

    private void SmoothMap()
    {
        for(int i = 0; i < smoothCycles; i++)
        {
            for(int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    int neighboringForestCount = GetNeighborsOfThisTileType(x, y, new int[] { (int)tileType.tree });

                    if(neighboringForestCount > neighborThreshold)
                    {
                        GameManager.instance.map[x, y] = (int)tileType.tree;
                    }
                    else if (neighboringForestCount < neighborThreshold)
                    {
                        GameManager.instance.map[x, y] = (int)tileType.grass;
                    }
                }
            }
        }
    }

    private int GetNeighborsOfThisTileType(int posInGridX, int posInGridY, int[] tiles)
    {
        int neighborsOfThisTile = 0;

        for(int x = posInGridX - 1; x <= posInGridX + 1; x++)
        {
            for (int y = posInGridY - 1; y <= posInGridY + 1; y++)
            {
                if(x > 0 && x < sizeX && y > 0 && y < sizeY && (y != posInGridY || x != posInGridX))
                {
                    foreach(int tile in tiles)
                    {
                        if (GameManager.instance.map[x, y] == tile)
                        {
                            neighborsOfThisTile++;
                        }
                    }
                }
            }
        }

        return neighborsOfThisTile;
    }
    /// <summary>
    /// Loops through the map array and spawns terrain
    /// </summary>
    private void SpawnTerrain()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                GameObject newTile = null;

                //64 PPU
                //float xPos = (float)x / 2 - sizeX / 4 + 0.5f;
                //float yPos = (float)y / 2 - sizeY / 4 + 0.25f;

                //32 PPU
                float xPos = (float)x - sizeX / 2;
                float yPos = (float)y - sizeY / 2 + 1;

                if (GameManager.instance.map[x,y] == (int)tileType.tree)
                {
                    //Instantiate(black, new Vector3(-sizeX/2 + x + 0.5f, -sizeY/2 + y + 0.5f, 0), Quaternion.identity);
                    GameObject tree = trees[Random.Range(0, trees.Length)];

                    newTile = Instantiate(tree, new Vector3(xPos, yPos, 0), Quaternion.identity);
                }
                else if(GameManager.instance.map[x, y] == (int)tileType.rock)
                {
                    newTile = Instantiate(rock, new Vector3(xPos, yPos, 0), Quaternion.identity);
                }
                else if(GameManager.instance.map[x,y] == (int)tileType.startRiver)
                {
                    Quaternion rotation;

                    if(x == 0)
                    {
                        rotation = Quaternion.Euler(0, 0, 90);
                    }
                    else if (x == sizeX - 1)
                    {
                        rotation = Quaternion.Euler(0, 0, -90);
                    }
                    else if(y == 0)
                    {
                        rotation = rotation = Quaternion.Euler(0, 0, 180);
                    }
                    else
                    {
                        rotation = Quaternion.identity;
                    }

                    newTile = Instantiate(riverStart, new Vector3(xPos, yPos, 0), rotation);
                }
                else if(GameManager.instance.map[x, y] == (int)tileType.straightRiver)
                {
                    Quaternion rotation = Quaternion.identity;

                    if(x > 0 && x < sizeX)
                    {
                        if (GameManager.instance.map[x - 1, y] == (int)tileType.straightRiver || GameManager.instance.map[x - 1, y] == (int)tileType.startRiver ||
                            GameManager.instance.map[x + 1, y] == (int)tileType.straightRiver || GameManager.instance.map[x + 1, y] == (int)tileType.startRiver ||
                            (GameManager.instance.map[x - 1, y] == (int)tileType.curveRiver && GameManager.instance.map[x + 1, y] == (int)tileType.endRiver) ||
                            (GameManager.instance.map[x - 1, y] == (int)tileType.endRiver && GameManager.instance.map[x + 1, y] == (int)tileType.curveRiver))
                        {
                            rotation = Quaternion.Euler(0, 0, 90);
                        }
                    }

                    newTile = isCurvedRiver(x, y, xPos, yPos);

                    if (newTile != null)
                    {
                        GameManager.instance.map[x, y] = (int)tileType.curveRiver;
                    }
                    else
                    {
                        newTile = Instantiate(riverStraight, new Vector3(xPos, yPos, 0), rotation);
                    }
                }

                else if (GameManager.instance.map[x, y] == (int)tileType.endRiver)
                {
                    Quaternion rotation;

                    if (x == 0)
                    {
                        rotation = Quaternion.Euler(0, 0, 90);
                    }
                    else if (x == sizeX - 1)
                    {
                        rotation = Quaternion.Euler(0, 0, -90);
                    }
                    else if (y == 0)
                    {
                        rotation = rotation = Quaternion.Euler(0, 0, 180);
                    }
                    else
                    {
                        rotation = Quaternion.identity;
                    }

                    newTile = Instantiate(riverEnd, new Vector3(xPos, yPos, 0), rotation);
                }
                else
                {
                    newTile = Instantiate(grass, new Vector3(xPos, yPos, 0), Quaternion.identity);
                }

                if(newTile != null)
                {
                    newTile.transform.parent = terrainContainer;
                }
            }
        }
    }

    private GameObject isCurvedRiver(int x, int y, float xPos, float yPos)
    {
        List<int> riverTiles = new List<int> { (int)tileType.curveRiver, (int)tileType.endRiver,
                                               (int)tileType.startRiver, (int)tileType.straightRiver};

        Debug.Log("isCurvedRiver called");

        if (x > 0 && x < sizeX && y > 0 && y < sizeY)
        {
            //down and left
            if(riverTiles.Contains(GameManager.instance.map[x - 1 ,y]) && riverTiles.Contains(GameManager.instance.map[x , y - 1]))
            {
                return Instantiate(riverCurve, new Vector3(xPos, yPos, 0), Quaternion.Euler(0,0, -90));
            }
            //down and right
            if (riverTiles.Contains(GameManager.instance.map[x, y - 1]) && riverTiles.Contains(GameManager.instance.map[x + 1, y]))
            {
                return Instantiate(riverCurve, new Vector3(xPos, yPos, 0), Quaternion.identity);
            }
            //up and right
            if (riverTiles.Contains(GameManager.instance.map[x + 1, y]) && riverTiles.Contains(GameManager.instance.map[x, y + 1]))
            {
                return Instantiate(riverCurve, new Vector3(xPos, yPos, 0), Quaternion.Euler(0, 0, 90));
            }
            //up and left
            if (riverTiles.Contains(GameManager.instance.map[x - 1, y]) && riverTiles.Contains(GameManager.instance.map[x, y + 1]))
            {
                return Instantiate(riverCurve, new Vector3(xPos, yPos, 0), Quaternion.Euler(0, 0, 180));
            }
        }

        return null;
    }

    private void DestroyTerrain()
    {
        foreach(Transform child in terrainContainer)
        {
            Destroy(child.gameObject);
        }
    }

}
