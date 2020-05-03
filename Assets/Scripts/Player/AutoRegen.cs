using UnityEngine;

public class AutoRegen : MonoBehaviour
{
    PlayerController player;

    public float timeDelta;
    public int inc;
    public int threshold;

    private float lastInc = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.Player;
    }

    // Update is called once per frame
    void FixedUpdate()
    {      
        if (RegenCond())
        {
            lastInc = (lastInc + Time.fixedDeltaTime) % timeDelta;
            if (lastInc <= Time.fixedDeltaTime)
            {
                player.UpdateHealth(inc);
            }
        }
        else
        {
            lastInc = 0;
        }
    }

    private bool RegenCond()
    {
        bool b = player.GetHunger() >= threshold && player.GetThirst() >= threshold && player.GetEnergy() >= threshold;
        b = b && player.GetHealth() < 100;
        return b;
    }
}
