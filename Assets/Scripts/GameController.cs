using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{

    [Tooltip("Contain the object player")] 
    public PlayerController player;
    
    [Space]
    [Range(0f, 24f)]
    public float gameClock = 8f;

    public TMP_Text clock;
    public GameObject BunkerPanel;

    // Start is called before the first frame update
    void Start() {
        //UpdateGameClock(gameClock);

        BunkerPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update() {
    }

    public void UpdateGameClock(float inc) {
        gameClock = (gameClock + inc) % 24f;
        double hours = Mathf.Floor(gameClock);
        double minutes = Mathf.Abs(Mathf.Ceil((gameClock - Mathf.Ceil(gameClock)) * 60));
        clock.SetText("Clock : " + hours.ToString("0") + "h" + minutes.ToString("0"));
        Debug.Log("Decrementing energy by : " + inc);
        player.UpdateEnergy(-1 * inc);
    } 
}
