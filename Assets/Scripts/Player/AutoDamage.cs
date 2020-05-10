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

    public static AutoDamage GetAutoDamage { get; private set; }

    [Range(0.5f, 8f)] public float deltaTime;
    [Range(1f, 25f)] public int inc;

    private float lastInc = 0f;

    void Awake()
    {
        GetAutoDamage = this;
    }

    // Update is called once per frame
    public void DoDamage(float elapsedTime)
    {
        if (DmgCond())
        {
            lastInc += elapsedTime;
            while (lastInc >= deltaTime)
            {
                float tmp = inc;
                if (PlayerController.Player.GetHunger() <= hungerThreshold) tmp *= hungerMultiplier;
                if (PlayerController.Player.GetThirst() <= thirstThreshold) tmp *= thirstMultiplier;
                if (PlayerController.Player.GetEnergy() <= energyThreshold) tmp *= energyMultiplier;
                PlayerController.Player.UpdateHealth(-1*(int)tmp);
                lastInc -= deltaTime;
            }
        }
        else
        {
            lastInc = 0;
        }
    }

    private bool DmgCond()
    {
        return PlayerController.Player.GetHunger() <= hungerThreshold || PlayerController.Player.GetThirst() <= thirstThreshold || PlayerController.Player.GetEnergy() <= energyThreshold;
    }
}
