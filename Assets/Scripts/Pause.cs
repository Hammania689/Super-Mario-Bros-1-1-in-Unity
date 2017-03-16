using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject player;
    
    Rigidbody2D rb;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
            gamePaused = !gamePaused;
        if (gamePaused == true)
            Time.timeScale = 0;
        else 
            Time.timeScale = 1;       
	}
}
