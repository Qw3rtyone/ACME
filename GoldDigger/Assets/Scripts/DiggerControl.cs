using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggerControl : MonoBehaviour {

    private const int UP = 1;
    private const int DOWN = 2;
    private const int LEFT = 3;
    private const int RIGHT = 4;

    Vector3 target = new Vector3(1,1,0);

    private FuelBehaviour fuelBar;
    private SpawnGrid grid;
    private GameObject refuel;
    // Use this for initialization
    void Start () {
        fuelBar = GameObject.FindGameObjectWithTag("FuelBar").GetComponent<FuelBehaviour>();
        grid = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnGrid>();
        refuel = GameObject.FindGameObjectWithTag("Refuel");
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
        if (this.transform.position.y != 1)
        {
            int currentFuel = fuelBar.fuel--;
            fuelBar.UpdateFuel();// (currentFuel);
        }
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
            int x = (int)this.transform.position.x;
            int y = (int)(this.transform.position.y + 1) * -1;
           
            if(y == -1 || grid.grid[x][y] == null)
            {
                this.transform.position += new Vector3(0, 1, 0);
                this.transform.rotation = new Quaternion(0, 0, 0, 0);
                FuelConsume();
            }
        }
        else
            Debug.Log("not");
        
    }

    private void FloatingButton()
    {

        if (Mathf.Round(refuel.transform.position.x) == Mathf.Round(target.x) || Mathf.Round(refuel.transform.position.y) == Mathf.Round(target.y))
            target = this.transform.position + new Vector3(2, 1, 0);

        refuel.transform.position = Vector3.Lerp(refuel.transform.position, target, Time.deltaTime * 1.0f);
        
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        if (this.transform.position.y == 1)
            refuel.SetActive(true);
        else
            refuel.SetActive(false);
        FloatingButton();
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,this.transform.position + new Vector3(0,0,-25), Time.deltaTime * 2.0f);
    }
        
}
