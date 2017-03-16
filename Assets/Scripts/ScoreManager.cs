using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    Text text;

    public static int score;

	// Use this for initialization
	void Start ()
    {
        text = GetComponent<Text>();
        score = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        text.text = "Score: " + score;
	}
}
