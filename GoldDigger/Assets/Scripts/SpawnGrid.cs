using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGrid : MonoBehaviour {

    public static int width = 85;
    public static int height = 170;

    private float diamondChance = 1, goldChance = 1;
    public GameObject[][] grid;
	// Use this for initialization.
	void Start () {
        GameObject Player = Instantiate(Resources.Load("Digger")) as GameObject;
        Player.AddComponent<DiggerControl>();
        Player.AddComponent<ColliderLogic>();
        Player.transform.position = new Vector3(5, 1, -1);

        Debug.Log("Start the spawning");
        SpawnMap();
    }
	
    /**
     * Create the game map and populate it with an appropriate number of each block
     */
    public void SpawnMap()
    {
        grid = new GameObject[width][];
        for (int x = 0; x < width; x++)
        {
            grid[x] = new GameObject[height];

            goldChance = diamondChance = 1;
            for (int y = 0; y < height; y++)
            {
                float spawnChance = Random.Range(0, 100);
                if (y % 10 == 0)
                {
                    goldChance += 0.05f;
                    diamondChance += 0.01f;
                }
                if(y % 20 == 0)
                {
                    diamondChance += 0.2f;
                }
                GameObject go;
                if(spawnChance < 2*diamondChance )
                    go = Instantiate(Resources.Load("Diamond")) as GameObject;
                else if(spawnChance > 2*goldChance && spawnChance < 9*goldChance)
                    go = Instantiate(Resources.Load("Gold")) as GameObject;
                else
                    go = Instantiate(Resources.Load("Dirt")) as GameObject;

                go.transform.position = new Vector3(x, -y, 0);
                
                grid[x][y] = go;
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
