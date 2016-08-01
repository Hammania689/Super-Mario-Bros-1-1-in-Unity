using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public GameObject LevelStart;
    public GameObject LevelEnd;
    float CamPos;
    float min;
    float max;

	// Use this for initialization
	void Start ()
    {
        min = LevelStart.transform.position.x + 12.7f;
        max = LevelEnd.transform.position.x - 12.7f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //The camera's x position is following the player and add space for the player to see what's coming next
        CamPos = player.transform.position.x + 7.6f;
        gameObject.transform.position = new Vector3(Mathf.Clamp(CamPos, min, max), gameObject.transform.position.y, gameObject.transform.position.z);
	}
}
