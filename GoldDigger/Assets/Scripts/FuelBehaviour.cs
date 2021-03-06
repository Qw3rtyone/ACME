﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FuelBehaviour : MonoBehaviour {

    public int fuel;
    public int dollars = 0;
    public Text fuelBar, dollarBar;
	// Use this for initialization.
	void Start () {
        //fuelBar = GameObject.FindGameObjectWithTag("FuelBar").GetComponent<Text>();
        fuelBar = this.gameObject.GetComponent<Text>();
        dollarBar = GameObject.FindGameObjectWithTag("Dollars").GetComponent<Text>();

        fuel = 35;
        fuelBar.text = "Fuel: " + fuel;
        dollarBar.text = "$ " + dollars;
    }
	/**
     * Update the fuel tab in the UI.
     */
    public void UpdateFuel()
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
        int points = 0;
        if(dollars >= 5)
        {
            points = -5;
            fuel += 10;
            UpdateFuel();
            UpdateDollars(points);
        }
    }

    /**
     * Update the current money earned by the player. Should be called from ColliderLogic
     * @param points The value to update the UI element with
     */
    public void UpdateDollars(int points)
    {
        dollars += points;
        dollarBar.text = "$ " + dollars;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
