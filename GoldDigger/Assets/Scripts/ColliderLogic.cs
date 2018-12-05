using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderLogic : MonoBehaviour {

    SpawnGrid gridSpawner;
    GameObject[][] grid;
    GameObject tile;
    public FuelBehaviour fuelBehaviour;

    public int x, y, points;
	// Use this for initialization
	void Start () {
        gridSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnGrid>();
        grid = gridSpawner.grid;
        fuelBehaviour = GameObject.FindGameObjectWithTag("FuelBar").GetComponent<FuelBehaviour>();
        points = fuelBehaviour.dollars;
	}
	
    /**
     * Collect the correct points depending on the block mined
     * @param block The block the player occupies.
     */
    public void Collect(string block)
    {
        if (block == "Gold")
            points += 10;
        else if (block == "Diamond")
            points += 20;
        else
            points += 0;

        fuelBehaviour.UpdateDollars(points);

    }
	// Update is called once per frame
	void Update () {
        x = (int) this.transform.position.x;
        y = (int) this.transform.position.y * -1;
        
        if (y > -1 && grid[x][y] != null)
        {
            tile = grid[x][y];
            Debug.Log(tile.tag);

            Collect(tile.tag);

            Destroy(tile);
        }
	}
}
