using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class ContractManager : Singleton<ContractManager>
{
    [SerializeField] private List<Contract> contractList = new List<Contract>();

    private ContractManagerUI contractManagerUI;
    private Contract currentContract;

    public event Action<int[]> OnContractPicked;

    public int[] CurrentContractRequirements => currentContract.Requirements;
    public int CurrentContractReward => currentContract.CompletionReward;

    protected override void Awake()
    {
        base.Awake();

        contractManagerUI = GetComponent<ContractManagerUI>();
    }
    private void Start()
    {
        GenerateContracts();
    }

    public void GenerateContracts()
    {
        foreach (var contract in contractList)
        {
            contract.GenerateContract();
        }
    }

    public void PickContract(int index)
    {
        AudioManager.Current.PlaySound(AudioClips.Button_SFX);

        currentContract = contractList[index];

        OnContractPicked?.Invoke(currentContract.Requirements);

        TimerManager.Instance.SetTimer(currentContract.BuildTime);
    }
}
