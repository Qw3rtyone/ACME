using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refuel : MonoBehaviour {

    FuelBehaviour fuelBehaviour;
	// Use this for initialization
	void Start () {
        fuelBehaviour = GameObject.FindGameObjectWithTag("FuelBar").GetComponent<FuelBehaviour>();
            
	}
	
    /**
     * Refuel the miner and update the moeny and fuel bars accordingly.
     */
    public void OnClickRefuel()
    {
        fuelBehaviour.Refuel();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
