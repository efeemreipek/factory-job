using UnityEngine;
using DG.Tweening;
using System;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private TextMeshProUGUI contractsMadeText;
    [SerializeField] private TextMeshProUGUI moneyEarnedText;

    public static event Action OnPlayButtonPressed;
    public static event Action OnPlayAgainButtonPressed;

    private void OnEnable()
    {
        TimerManager.Current.OnTimerEnded += TimerManager_OnTimerEnded;
    }
    private void OnDisable()
    {
        TimerManager.Current.OnTimerEnded -= TimerManager_OnTimerEnded;
    }

    private void TimerManager_OnTimerEnded()
    {
        contractsMadeText.text = $"Contracts Made: {ContractStatusChecker.ContractsMade}";
        moneyEarnedText.text = $"Money Earned: {RewardUI.CurrentReward}";
    }

    public void PlayButton()
    {
        AudioManager.Current.PlaySound(AudioClips.Button_SFX);

        titlePanel.transform
            .DOScale(Vector3.zero, 0.5f)
            .SetEase(Ease.InOutQuint)
            .OnComplete(() => 
            {
                titlePanel.SetActive(false);
                OnPlayButtonPressed?.Invoke();
            });
    }
    public void QuitButton()
    {
        AudioManager.Current.PlaySound(AudioClips.Button_SFX);

        Application.Quit();
    }
    public void PlayAgainButton()
    {
        AudioManager.Current.PlaySound(AudioClips.Button_SFX);

        OnPlayAgainButtonPressed?.Invoke();
    }
}
