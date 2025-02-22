using System;
using UnityEngine;

public class ContractStatusChecker : MonoBehaviour
{
    public static event Action<int> OnContractOver;

    private bool isContractOver = false;
    private int[] requirements = new int[4];

    public static int ContractsMade = 0;

    private void Start()
    {
        Inventory.Current.OnInventoryChanged += Inventory_OnInventoryChanged;
        ContractManager.Current.OnContractPicked += ContractManager_OnContractPicked;
        MenuManager.OnPlayAgainButtonPressed += MenuManager_OnPlayAgainButtonPressed;
    }
    private void OnDestroy()
    {
        Inventory.Current.OnInventoryChanged -= Inventory_OnInventoryChanged;
        ContractManager.Current.OnContractPicked -= ContractManager_OnContractPicked;
        MenuManager.OnPlayAgainButtonPressed -= MenuManager_OnPlayAgainButtonPressed;
    }

    private void ContractManager_OnContractPicked(int[] obj)
    {
        requirements = obj;
        isContractOver = false;
    }
    private void Inventory_OnInventoryChanged(int index, int amount)
    {
        bool allRequirementsMet = true;
        for(int i = 0; i < requirements.Length; i++)
        {
            if(Inventory.Current.CheckItemAmount(i) < requirements[i])
            {
                allRequirementsMet = false;
                break;
            }
        }

        if(allRequirementsMet && !isContractOver)
        {
            isContractOver = true;
            ContractsMade++;
            OnContractOver?.Invoke(ContractManager.Current.CurrentContractReward);
            TimerManager.Current.StopTimer();
            AudioManager.Current.PlaySound(AudioClips.ContractComplete_SFX, 0.3f);
            ContractManager.Current.GenerateContracts();
        }
    }
    private void MenuManager_OnPlayAgainButtonPressed()
    {
        ContractsMade = 0;
    }
}
