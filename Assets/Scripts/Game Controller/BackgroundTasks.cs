using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTasks : MonoBehaviour
{
    public static BackgroundTasks BgTasks { get; private set; }

    public bool isSleeping = false;
    public int sleepInc = 1;
    public float hungerMultiplier = 0.3f;
    public float thirstMultiplier = 0.4f;

    private float hStep = 0f;
    private float tStep = 0f;
    private float energyTarget = 0f;
    // Start is called before the first frame update
    void Start()
    {
        BgTasks = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSleeping)
        {
            hStep += sleepInc * hungerMultiplier;
            tStep += sleepInc * thirstMultiplier;
            BackgroundSleep();
        }
    }

    public void Sleep(float sleepTime)
    {
        isSleeping = true;
        //this.sleepTime = sleepTime;
        // 6.25 EP per hour <=> 50 EP per 8 hours
        energyTarget = PlayerController.Player.GetEnergy() + Mathf.FloorToInt((Mathf.Floor(sleepTime) + Mathf.Floor((sleepTime - Mathf.Floor(sleepTime)))) * 60f / 9.6f);
    }

    private void BackgroundSleep()
    {
        if (PlayerController.Player.GetEnergy() < energyTarget)
        {
            PlayerController.Player.UpdateEnergy(sleepInc);
            if (hStep >= 1f)
            {
                PlayerController.Player.UpdateHunger(-1 * (int)hStep);
                hStep = 0f;
            }
            if (tStep >= 1f)
            {
                PlayerController.Player.UpdateThirst(-1 * (int)tStep);
                tStep = 0f;
            }
        }
        else
        {
            isSleeping = false;
            hStep = 0f;
            tStep = 0f;
            energyTarget = 0f;
        }
    }

    public void Use(Consumable toUse, float delay = 0f)
    {
        StartCoroutine(DelayedUse(delay, toUse));
    }

    private IEnumerator DelayedUse(float delay, Consumable cons)
    {

        Debug.Log("Delayed use ...");
        yield return new WaitForSeconds(delay);

        PlayerController.Player.UpdateEnergy(cons.GetEnergy());
        PlayerController.Player.UpdateHealth(cons.GetHealth());
        PlayerController.Player.UpdateHunger(cons.GetHunger());
        PlayerController.Player.UpdateThirst(cons.GetThirst());

        yield return null;
    }
}
