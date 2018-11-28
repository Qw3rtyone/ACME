using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBehaviour : MonoBehaviour {

    public int fuel;
    public Text fuelBar;
	// Use this for initialization
	void Start () {
        fuel = 35;
        fuelBar.text = "Fuels: " + fuel;
    }
	
    public void UpdateFuel(int fuel)
    {
        fuelBar.text = "Fuel: " + fuel;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
