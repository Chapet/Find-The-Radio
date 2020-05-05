using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDamage : MonoBehaviour
{
    [Range(1f, 2f)] public float hungerMultiplier;
    [Range(1f, 2f)] public float thirstMultiplier;
    [Range(1f, 2f)] public float energyMultiplier;

    [Range(0, 100)] public int hungerThreshold;
    [Range(0, 100)] public int thirstThreshold;
    [Range(0, 100)] public int energyThreshold;

    public float timeDelta;
    public int inc;
    public float lastInc = 0;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (DmgCond())
        {
            lastInc = (lastInc + Time.fixedDeltaTime) % timeDelta;
            if (lastInc <= Time.fixedDeltaTime)
            {
                float tmp = inc;
                if (player.GetHunger() <= hungerThreshold) tmp *= hungerMultiplier;
                if (player.GetThirst() <= thirstThreshold) tmp *= thirstMultiplier;
                if (player.GetEnergy() <= energyThreshold) tmp *= energyMultiplier;
                player.UpdateHealth(-1*(int)tmp);
            }
        }
        else
        {
            lastInc = 0;
        }
    }

    private bool DmgCond()
    {
        return player.GetHunger() <= hungerThreshold || player.GetThirst() <= thirstThreshold || player.GetEnergy() <= energyThreshold;
    }
}
