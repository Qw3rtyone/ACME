using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggerControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    /**
     * Controls movement of the player sprite. 
     * Standard Controls (arrows + wasd). Only moves one block per press
     */
    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            this.transform.position += new Vector3(0, -1, 0);
            Debug.Log("Down");
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 90);
            this.transform.position += new Vector3(1, 0, 0);
            Debug.Log("Right");
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.rotation = Quaternion.Euler(0, 0, -90);
            this.transform.position += new Vector3(-1, 0, 0);
            Debug.Log("Left");
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
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
    }
        
}
