using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDamage : MonoBehaviour
{
    PlayerController player;

    public float timeDelta = 7f;
    private int energyInc = -1;
    private int hungerInc = -1;
    private int thirstInc = -1;

    
    public float lastInc = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.Player;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.GetHunger() <= 0 || player.GetThirst() <= 0 || player.GetEnergy() <= 0)
        {
            lastInc = (lastInc + Time.fixedDeltaTime) % timeDelta;
            if (lastInc <= Time.fixedDeltaTime)
            {
                DoDamage();
            }    
        }
    }
    
    private void DoDamage()
    {
        if (player.GetHunger() <= 0)
        {
            player.UpdateHealth(hungerInc);
        }

        if (player.GetThirst() <= 0)
        {
            player.UpdateHealth(thirstInc);
        }

        if (player.GetEnergy() <= 0)
        {
            player.UpdateHealth(energyInc);
        }
    }
}
