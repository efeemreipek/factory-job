using TMPro;
using UnityEngine;

public class RewardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rewardText;

    public static int CurrentReward = 0;

    private void OnEnable()
    {
        UIAnimator.OnRewardAdded += UIAnimator_OnRewardAdded;
        MenuManager.OnPlayAgainButtonPressed += MenuManager_OnPlayAgainButtonPressed;
    }
    private void OnDisable()
    {
        UIAnimator.OnRewardAdded -= UIAnimator_OnRewardAdded;
        MenuManager.OnPlayAgainButtonPressed -= MenuManager_OnPlayAgainButtonPressed;
    }
    private void Start()
    {
        rewardText.text = CurrentReward.ToString();
    }
    private void UIAnimator_OnRewardAdded(int rewardAmount)
    {
        CurrentReward += rewardAmount;
        rewardText.text = CurrentReward.ToString();
    }
    private void MenuManager_OnPlayAgainButtonPressed()
    {
        CurrentReward = 0;
    }
}
