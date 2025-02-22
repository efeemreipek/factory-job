using System;
using System.Collections.Generic;
using UnityEngine;

public class MachineSelector : MonoBehaviour
{
    [SerializeField] private List<GameObject> machinePrefabList = new List<GameObject>();

    public event Action<GameObject> OnMachineSelected;

    private void OnEnable()
    {
        TimerManager.Current.OnTimerEnded += SelectNull;
        ControlPanel.OnStartButtonPressed += SelectNull;
    }
    private void OnDisable()
    {
        TimerManager.Current.OnTimerEnded -= SelectNull;
        ControlPanel.OnStartButtonPressed -= SelectNull;
    }

    private void SelectNull() => SelectMachinePrefab(-1);

    public void SelectMachinePrefab(int machineIndex)
    {
        if(machineIndex == -1)
        {
            OnMachineSelected?.Invoke(null);
            return;
        }
        OnMachineSelected?.Invoke(machinePrefabList[machineIndex]);
    }
}
