using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTasks : MonoBehaviour
{
    public static BackgroundTasks Tasks { get; private set; }

    public bool IsSleeping { get; private set; }
    private DateTime startSleeping;
    private DateTime endSleeping;
    public float updateStep;
    public int sleepInc;
    public float hungerMultiplier;
    public float thirstMultiplier;

    public bool IsScavenging { get; set; }
    public DateTime StartScavenging { get; private set; }
    public DateTime EndScavenging { get; set; }

    private float hStep = 0f;
    private float tStep = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Tasks = this;
        IsSleeping = false;
        IsScavenging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSleeping)
        {
            var diffInSeconds = (DateTime.Now - startSleeping).TotalSeconds;
            if (diffInSeconds > updateStep)
            {
                hStep += sleepInc * hungerMultiplier;
                tStep += sleepInc * thirstMultiplier;
                BackgroundSleep();
            }          
        }
        if (IsScavenging)
        {
            if(DateTime.Now > EndScavenging)
            {
                IsScavenging = false;
                Debug.Log("Returning from scavenging :-)");
            }
        }
    }

    public void Scavenge(float scavengeTime)
    {
        IsScavenging = true;
        StartScavenging = DateTime.Now;
        // Real implem : endTime = startTime.AddMinutes(scavengeTime);
        // Debug.Log("scavengeTime : " + scavengeTime);
        EndScavenging = StartScavenging.AddSeconds(2*scavengeTime); // For testing
    }

    public void Sleep(float sleepTime)
    {
        IsSleeping = true;
        startSleeping = DateTime.Now;
        endSleeping = startSleeping.AddSeconds(sleepTime * updateStep);
    }

    private void BackgroundSleep()
    {
        if (startSleeping < endSleeping)
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
            startSleeping = startSleeping.AddSeconds(updateStep);
            GameController.Controller.UpdateGameClock(1);
        }
        else
        {
            IsSleeping = false;
            hStep = 0f;
            tStep = 0f;
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
