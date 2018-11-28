using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggerControl : MonoBehaviour {

    private const int UP = 1;
    private const int DOWN = 2;
    private const int LEFT = 3;
    private const int RIGHT = 4;

    private FuelBehaviour fuelBar;
    // Use this for initialization
    void Start () {
        fuelBar = GameObject.FindGameObjectWithTag("FuelBar").GetComponent<FuelBehaviour>();
	}

    /**
     * Checks if the the digger is outside the grid. 
     * returns a boolean value
     */
    private bool IsOutOfGrid(float x, float y, int dir)
    {
        if (dir == UP && (y >= 1))
            return true;
        else if (dir == DOWN && (y <= -169))
            return true;
        else if (dir == LEFT && (x <= 0))
            return true;
        else if (dir == RIGHT && (x >= 84))
            return true;
        else
            return false;
    }

    /*
     * Reduces fuel available to the player on each movement
     */
    private void FuelConsume()
    {
        int currentFuel = fuelBar.fuel--;
        fuelBar.UpdateFuel(currentFuel);
    }
    
    /**
     * Controls movement of the player sprite. 
     * Standard Controls (arrows + wasd). Only moves one block per press
     */
    private void Movement()
    {
        if (!IsOutOfGrid(this.transform.position.x, this.transform.position.y, DOWN) && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            this.transform.position += new Vector3(0, -1, 0);
            FuelConsume();
            Debug.Log("Down");
        }
        else if (!IsOutOfGrid(this.transform.position.x, this.transform.position.y, RIGHT) && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 90);
            this.transform.position += new Vector3(1, 0, 0);
            FuelConsume();
            Debug.Log("Right");
        }
        else if (!IsOutOfGrid(this.transform.position.x, this.transform.position.y, LEFT) && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            this.transform.rotation = Quaternion.Euler(0, 0, -90);
            this.transform.position += new Vector3(-1, 0, 0);
            FuelConsume();
            Debug.Log("Left");
        }
        else if (!IsOutOfGrid(this.transform.position.x, this.transform.position.y, UP) && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            //To-do. Check if block above has been dug before moving up.
        }
        else
            Debug.Log("not");
        
    }

// Update is called once per frame
    void Update()
    {
        Movement();
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,this.transform.position + new Vector3(0,0,-25), Time.deltaTime * 2.0f);
    }
        
}
