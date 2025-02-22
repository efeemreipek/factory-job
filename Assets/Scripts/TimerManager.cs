using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class TimerManager : Singleton<TimerManager>
{
    [SerializeField] private float productionTime = 30f;

    private float timer = 0f;
    private float currentTime;
    private bool isTimerOn = false;

    public event Action<float> OnTimePassed;
    public event Action OnTimerEnded;

    public bool IsTimerOn => isTimerOn;

    private void Update()
    {
        if(!isTimerOn) return;

        timer += Time.deltaTime;

        if(timer >= 1f)
        {
            timer = 0f;
            currentTime--;

            if(currentTime >= 0f)
            {
                OnTimePassed?.Invoke(currentTime);
            }
            if(currentTime <= 0f)
            {
                currentTime = 0f;
                isTimerOn = false;
                AudioManager.Current.PlaySound(AudioClips.TimeLimitExceeded_SFX, 0.3f);
                ContractManager.Current.GenerateContracts();
                OnTimerEnded?.Invoke();
            }
        }
    }

    public void SetTimer(float time)
    {
        currentTime = time;
        OnTimePassed?.Invoke(currentTime);
        isTimerOn = true;
    }
    public void StopTimer()
    {
        isTimerOn = false;
    }
    public void StartProductionTimer()
    {
        SetTimer(productionTime);
    }
}
