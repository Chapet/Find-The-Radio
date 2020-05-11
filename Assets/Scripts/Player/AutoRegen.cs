using UnityEngine;

public class AutoRegen : MonoBehaviour
{
    public static AutoRegen GetAutoRegen { get; private set; }

    [Range(0.5f, 8f)] public float deltaTime;
    [Range(0, 100)] public int threshold;
    [Range(1, 25)] public int inc;

    private float lastInc = 0f;

    void Awake()
    {
        GetAutoRegen = this;
    }

    // Update is called once per frame
    public void DoRegen(float timeElapsed)
    {      
        if (RegenCond())
        {
            lastInc += timeElapsed;
            while (lastInc >= deltaTime)
            {
                PlayerController.Player.UpdateHealth(inc);
                lastInc -= deltaTime;
            }
        }
        else
        {
            lastInc = 0f;
        }
    }

    private bool RegenCond()
    {
        bool b = PlayerController.Player.GetHunger() >= threshold && PlayerController.Player.GetThirst() >= threshold && PlayerController.Player.GetEnergy() >= threshold;
        b = b && PlayerController.Player.GetHealth() < 100;
        return b;
    }
}
