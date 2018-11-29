using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FuelBehaviour : MonoBehaviour {

    public int fuel;
    public int dollars = 0;
    public Text fuelBar, dollarBar;
	// Use this for initialization
	void Start () {
        fuelBar = GameObject.FindGameObjectWithTag("FuelBar").GetComponent<Text>();
        dollarBar = GameObject.FindGameObjectWithTag("Dollars").GetComponent<Text>();

        fuel = 35;
        fuelBar.text = "Fuels: " + fuel;
        dollarBar.text = "$ " + dollars;
    }
	/**
     * Update the fuel tab in the UI.
     */
    public void UpdateFuel()//int fuel)
    {
        if (fuel > 0)
            fuelBar.text = "Fuel: " + fuel;
        else
            SceneManager.LoadScene(0); //End the game. Do  this is a better way. DONT LEAVE IT
    }

    /**
     * Refuel once the player is back of the surface
     */
    public void Refuel()
    {
        if(dollars >= 5)
        {
            dollars -= 5;
            fuel += 10;
            UpdateFuel();//fuel);
            UpdateDollars(dollars);
        }
    }

    /**
     * Update the current money earned by the player. Should be called from ColliderLogic
     */
    public void UpdateDollars(int points)
    {
        dollars = points;
        dollarBar.text = "$ " + dollars;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
