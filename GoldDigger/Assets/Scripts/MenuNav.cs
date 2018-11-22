using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNav : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /**
     * Go to the GameScene when the play button is clicked
     */
    public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
    }
    /**
     * Quit the application when the quit button is clicked
     */
    public void OnClickQuit()
    {
        Application.Quit();
    }
}
