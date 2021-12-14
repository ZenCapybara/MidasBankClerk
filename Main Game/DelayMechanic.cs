using UnityEngine;
using System.Collections.Generic;
using static Delay;

public class DelayMechanic : MonoBehaviour
{
    private float countdown = 0;
    List<Delay> cuedDelays;
    private MidasOS pcInterface;
    private DeliverObjectsMechanics deliverableObjects;

    private void Start()
    {
        cuedDelays = new List<Delay>();
        pcInterface = ScriptFinder.Get<MidasOS>();
        deliverableObjects = ScriptFinder.Get<DeliverObjectsMechanics>();
    }

    /// <summary>
    /// Request delay for X seconds.
    /// </summary>
    public void RequestDelayCountdown(float seconds, string pcStandbyMessage)
    {
        cuedDelays.Add(new Delay(YieldType.CountDown, pcStandbyMessage));
        if (cuedDelays.Count == 1)
            StartDelay();
    }

    /// <summary>
    /// Request delay until "YieldDelay()" is called.
    /// </summary>
    public void RequestDelay(string pcStandbyMessage)
    {
        cuedDelays.Add(new Delay(YieldType.ExternalCall, pcStandbyMessage));
        if (cuedDelays.Count == 1)
            StartDelay();
    }

    public void YieldDelay()
    {
        CallNextInCue();
    }

    private void CallNextInCue()
    {
        cuedDelays.RemoveAt(0);
        if (cuedDelays.Count == 0)
        {
            UnlockThings();
            return;
        }
        StartDelay();
    }

    private void StartDelay()
    {
        if (cuedDelays[0].yieldType == YieldType.CountDown)
            countdown = cuedDelays[0].countdown;
        LockThings();

    }

    private void LockThings()
    {
        pcInterface.LockPC(cuedDelays[0].pcStandbyMessage);
        deliverableObjects.DeliverLock();
    }

    private void UnlockThings()
    {
        pcInterface.UnlockPC();
        deliverableObjects.DeliverUnlock();
    }

    private void Countdown()
    {
        if (cuedDelays.Count == 0
            || cuedDelays[0].yieldType == Delay.YieldType.ExternalCall)
        {
            return;
        }

        countdown -= Time.deltaTime;

        if (countdown < 0)
        {
            CallNextInCue();
        }
    }

    //Update is called every frame by Unity.Engine
    private void Update()
    {
        if (cuedDelays.Count > 0)
            Countdown();
    }
}

class Delay
{
    public enum YieldType
    {
        CountDown,
        ExternalCall
    }

    public YieldType yieldType { get; }
    public string pcStandbyMessage { get; }
    public float countdown { get; }

    /// <summary>
    /// Countdown is not optional if delay type is "ExternalCall"
    /// </summary>
    public Delay(YieldType yieldType, string pcStandbyMessage, float countdown = 0)
    {
        this.yieldType = yieldType;
        this.pcStandbyMessage = pcStandbyMessage;
        if (yieldType == YieldType.CountDown)
        {
            this.countdown = countdown;
            if (countdown == 0)
                Debug.LogWarning("Started a Countdown Delay with Delay == 0");
        }
    }

}