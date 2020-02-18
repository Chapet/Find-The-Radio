using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameController : MonoBehaviour
{
    [Range(0f, 24f)]
    public float gameClock;

    public TMP_Text clock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGameClock(float inc)
    {
        gameClock = (gameClock + inc) % 24f;
        clock.SetText("Time : " + gameClock.ToString("0.0") + " h.");
    }
}
