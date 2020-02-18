using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Range(0f, 24f)]
    public float gameClock;

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
        // Update UI
    }
}
