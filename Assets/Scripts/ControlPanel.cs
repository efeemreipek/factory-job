using System;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public static event Action OnStartButtonPressed;

    public void StartButton()
    {
        Supplier[] allSuppliers = FindObjectsByType<Supplier>(FindObjectsSortMode.None);
        foreach(Supplier supplier in allSuppliers)
        {
            supplier.IsActive = true;
        }

        TimerManager.Current.StartProductionTimer();
        OnStartButtonPressed?.Invoke();
    }

    public void ClearLayoutButton()
    {
        BaseMachine[] allMachines = FindObjectsByType<BaseMachine>(FindObjectsSortMode.None);
        foreach (BaseMachine machine in allMachines)
        {
            Destroy(machine.gameObject);
        }
    }
}
