using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTime : MonoBehaviour
{
    public float levelTime = 396;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        levelTime -= Time.deltaTime;
        text.text = "Time \n" + (int) levelTime;
    }
}
