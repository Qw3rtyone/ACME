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
     * @param x The current x position of the player
     * @param y The current y position of the player
     * @param dir The direction the player wants to move
     * @return True if one the edge of the grid. False otherwise
     */
    public bool IsOutOfGrid(float x, float y, int dir)
    {
        if (dir == UP && (y >= 1))
            return true;
        else if (dir == DOWN && (y <= (SpawnGrid.height - 1)*-1))
            return true;
        else if (dir == LEFT && (x <= 0))
            return true;
        else if (dir == RIGHT && (x >= (SpawnGrid.width -1)))
            return true;
        else
            return false;
    }

    /*
     * Reduces fuel available to the player on each movement
     */
    private void FuelConsume(FuelBehaviour fuelBar)
    {
        if (this.transform.position.y != 1)
        {
            fuelBar.fuel--;
            fuelBar.UpdateFuel();
        }
    }
    //Test the private function Fuel consume
    public GameObject TestFuelConsume(int fuel, GameObject go)
    {
        go.GetComponent<FuelBehaviour>().fuel = fuel;
        FuelConsume(go.GetComponent<FuelBehaviour>());
        return go;
    }

    public Vector3 TestMovement(string direction, GameObject go)
    {
        Vector3 result = new Vector3(0, 0, 0);
        FuelBehaviour fb = go.GetComponent<FuelBehaviour>();
        fb.fuel = 2;
        if (direction == "up")
        {
            Movement(new Vector3(0, 1, 0), Quaternion.Euler(0, 0, 0), fb);
            result = this.transform.position;
        }
        else if(direction == "down")
        {
            Movement(new Vector3(0, -1, 0), Quaternion.Euler(0, 0, 0), fb);
            result = this.transform.position;
        }
        else if(direction == "left")
        {
            Movement(new Vector3(-1, 0, 0), Quaternion.Euler(0, 0, -90), fb);
            result = this.transform.position;
        }
        else if(direction == "right")
        {
            Movement(new Vector3(1, 0, 0), Quaternion.Euler(0, 0, 90), fb);
            result = this.transform.position;
        }
        return result;
    }
    /**
     * Controls movement of the player sprite. 
     * Standard Controls (arrows + wasd). Only moves one block per press
     */
    private void Movement(Vector3 position, Quaternion rotation, FuelBehaviour fuelbar)
    {
        if (position != null)
        {
            this.transform.rotation = rotation;
            this.transform.position += position;
            FuelConsume(fuelbar);

        }
        else
            Debug.Log("not");
        
    }

    /**
     * The floating UI element that's only present when the player is on the 'surface'
     */
    private void FloatingButton()
    {

        if (Mathf.Round(refuel.transform.position.x) == Mathf.Round(target.x) || Mathf.Round(refuel.transform.position.y) == Mathf.Round(target.y))
            target = this.transform.position + new Vector3(2, 1, 0);

        refuel.transform.position = Vector3.Lerp(refuel.transform.position, target, Time.deltaTime * 1.0f);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsOutOfGrid(this.transform.position.x, this.transform.position.y, DOWN) && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
            Movement(new Vector3(0, -1, 0), Quaternion.Euler(0, 0, 0), this.fuelBar);
        else if (!IsOutOfGrid(this.transform.position.x, this.transform.position.y, RIGHT) && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
            Movement(new Vector3(1, 0, 0), Quaternion.Euler(0, 0, 90), this.fuelBar);
        else if (!IsOutOfGrid(this.transform.position.x, this.transform.position.y, LEFT) && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
            Movement(new Vector3(-1, 0, 0), Quaternion.Euler(0, 0, -90), this.fuelBar);
        else if (!IsOutOfGrid(this.transform.position.x, this.transform.position.y, UP) && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            int x = (int)this.transform.position.x;
            int y = (int)(this.transform.position.y + 1) * -1;

            if (y == -1 || grid.grid[x][y] == null)
                Movement(new Vector3(0, 1, 0), Quaternion.Euler(0, 0, 0), this.fuelBar);
        }

        if (this.transform.position.y == 1)
            refuel.SetActive(true);
        else
            refuel.SetActive(false);

        FloatingButton();
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,this.transform.position + new Vector3(0,0,-25), Time.deltaTime * 2.0f);
    }
        
}
