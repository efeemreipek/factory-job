using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private void OnEnable()
    {
        TimerManager.Current.OnTimePassed += TimerManager_OnTimePassed;
    }
    private void OnDisable()
    {
        TimerManager.Current.OnTimePassed -= TimerManager_OnTimePassed;   
    }

    private void TimerManager_OnTimePassed(float timeRemaining)
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
