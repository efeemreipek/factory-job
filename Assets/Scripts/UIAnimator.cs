using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;

public class UIAnimator : MonoBehaviour
{
    [SerializeField] private RectTransform machineSelectPanel;
    [SerializeField] private RectTransform inventoryPanel;
    [SerializeField] private RectTransform timePanel;
    [SerializeField] private RectTransform controlsPanel;
    [SerializeField] private RectTransform contractPanel;
    [SerializeField] private RectTransform rewardPanel;
    [SerializeField] private RectTransform losePanel;

    [SerializeField] private float animationSpeed = 0.5f;
    [SerializeField] private Ease animationEase = Ease.Linear;

    [SerializeField] private GameObject rewardPrefab;
    [SerializeField] private RectTransform rewardContainer; 

    private Vector2 machineSelectPanelOnScreen = new Vector2(20f, 10f);
    private Vector2 machineSelectPanelOffScreen = new Vector2(-240f, 10f);

    private Vector2 inventoryPanelOnScreen = new Vector2(20f, -20f);
    private Vector2 inventoryPanelOffScreen = new Vector2(-830f, -20f);

    private Vector2 timePanelOnScreen = new Vector2(-20f, -20f);
    private Vector2 timePanelOffScreen = new Vector2(470f, -20f);

    private Vector2 controlsPanelOnScreen = new Vector2(-20f, 20f);
    private Vector2 controlsPanelOffScreen = new Vector2(240f, 20f);

    private Vector2 contractPanelOnScreen = new Vector2(0f, -30f);
    private Vector2 contractPanelOffScreen = new Vector2(0f, 980f);

    private Vector2 rewardPanelOnScreen = new Vector2(-490f, -20f);
    private Vector2 rewardPanelOffScreen = new Vector2(-490f, 120f);

    private Vector2 losePanelOnScreen = new Vector2(0f, 0f);
    private Vector2 losePanelOffScreen = new Vector2(0f, 775f);

    public static event Action<int> OnRewardAdded;

    private void OnEnable()
    {
        ContractManager.Current.OnContractPicked += ContractManager_OnContractPicked;
        TimerManager.Current.OnTimerEnded += TimerManager_OnTimerEnded;
        MenuManager.OnPlayButtonPressed += MenuManager_OnPlayButtonPressed;
        ControlPanel.OnStartButtonPressed += ControlPanel_OnStartButtonPressed;
        ContractStatusChecker.OnContractOver += ContractStatusChecker_OnContractOver;
        MenuManager.OnPlayAgainButtonPressed += MenuManager_OnPlayAgainButtonPressed;
    }
    private void OnDisable()
    {
        ContractManager.Current.OnContractPicked -= ContractManager_OnContractPicked;
        TimerManager.Current.OnTimerEnded -= TimerManager_OnTimerEnded;
        MenuManager.OnPlayButtonPressed -= MenuManager_OnPlayButtonPressed;
        ControlPanel.OnStartButtonPressed -= ControlPanel_OnStartButtonPressed;
        ContractStatusChecker.OnContractOver -= ContractStatusChecker_OnContractOver;
        MenuManager.OnPlayAgainButtonPressed -= MenuManager_OnPlayAgainButtonPressed;
    }

    private void ContractManager_OnContractPicked(int[] obj)
    {
        machineSelectPanel.DOAnchorPos(machineSelectPanelOnScreen, animationSpeed).SetEase(animationEase);
        inventoryPanel.DOAnchorPos(inventoryPanelOnScreen, animationSpeed).SetEase(animationEase);
        timePanel.DOAnchorPos(timePanelOnScreen, animationSpeed).SetEase(animationEase);
        controlsPanel.DOAnchorPos(controlsPanelOnScreen, animationSpeed).SetEase(animationEase);

        contractPanel.DOAnchorPos(contractPanelOffScreen, animationSpeed).SetEase(animationEase);
    }
    private void TimerManager_OnTimerEnded()
    {
        machineSelectPanel.DOAnchorPos(machineSelectPanelOffScreen, animationSpeed).SetEase(animationEase);
        inventoryPanel.DOAnchorPos(inventoryPanelOffScreen, animationSpeed).SetEase(animationEase);
        controlsPanel.DOAnchorPos(controlsPanelOffScreen, animationSpeed).SetEase(animationEase);

        losePanel.DOAnchorPos(losePanelOnScreen, animationSpeed).SetEase(animationEase);
    }
    private void MenuManager_OnPlayButtonPressed()
    {
        contractPanel.DOAnchorPos(contractPanelOnScreen, animationSpeed).SetEase(animationEase);
    }
    private void ControlPanel_OnStartButtonPressed()
    {
        machineSelectPanel.DOAnchorPos(machineSelectPanelOffScreen, animationSpeed).SetEase(animationEase);
        controlsPanel.DOAnchorPos(controlsPanelOffScreen, animationSpeed).SetEase(animationEase);
    }
    private void MenuManager_OnPlayAgainButtonPressed()
    {
        timePanel.DOAnchorPos(timePanelOffScreen, animationSpeed).SetEase(animationEase);
        losePanel.DOAnchorPos(losePanelOffScreen, animationSpeed).SetEase(animationEase);

        contractPanel.DOAnchorPos(contractPanelOnScreen, animationSpeed).SetEase(animationEase);
    }

    private void ContractStatusChecker_OnContractOver(int rewardAmount)
    {
        StartCoroutine(RewardAnimation(rewardAmount));  
    }
    private IEnumerator RewardAnimation(int rewardAmount)
    {
        inventoryPanel.DOAnchorPos(inventoryPanelOffScreen, animationSpeed).SetEase(animationEase);

        rewardPanel.DOAnchorPos(rewardPanelOnScreen, animationSpeed).SetEase(animationEase);
        yield return new WaitForSeconds(animationSpeed);

        int rewardImageCount = rewardAmount / 10;

        for(int i = 0; i < rewardImageCount; i++)
        {
            GameObject reward = Instantiate(rewardPrefab, rewardContainer);
            RectTransform rewardTransform = reward.GetComponent<RectTransform>();

            rewardTransform.anchoredPosition = new Vector2(UnityEngine.Random.Range(-100, 100), 0f);

            rewardTransform.DOAnchorPos(new Vector2(370f, 470f), 1f).SetEase(Ease.InBack)
                .OnComplete(() => 
                {
                    AudioManager.Current.PlaySound(AudioClips.Reward_SFX, 0.2f);
                    Destroy(rewardTransform.gameObject); 
                });
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);
        
        OnRewardAdded?.Invoke(rewardAmount);

        yield return new WaitForSeconds(animationSpeed);

        rewardPanel.DOAnchorPos(rewardPanelOffScreen, animationSpeed).SetEase(animationEase);
        timePanel.DOAnchorPos(timePanelOffScreen, animationSpeed).SetEase(animationEase);

        yield return new WaitForSeconds(animationSpeed);

        contractPanel.DOAnchorPos(contractPanelOnScreen, animationSpeed).SetEase(animationEase);
    }
}
