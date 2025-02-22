using UnityEngine;

public class ClearLevel : MonoBehaviour
{
    private void OnEnable()
    {
        //TimerManager.Current.OnTimerEnded += Clear;
        ContractStatusChecker.OnContractOver += Clear;
        MenuManager.OnPlayAgainButtonPressed += Clear;
    }
    private void OnDisable()
    {
        //TimerManager.Current.OnTimerEnded -= Clear;
        ContractStatusChecker.OnContractOver -= Clear;
        MenuManager.OnPlayAgainButtonPressed -= Clear;
    }

    private void Clear()
    {
        BaseMachine[] allMachines = FindObjectsByType<BaseMachine>(FindObjectsSortMode.None);
        foreach(BaseMachine machine in allMachines)
        {
            Destroy(machine.gameObject);
        }

        ConveyorItem[] allItems = FindObjectsByType<ConveyorItem>(FindObjectsSortMode.None);
        foreach(ConveyorItem item in allItems)
        {
            Destroy(item.gameObject);
        }

        Inventory.Current.ClearInventory();
    }
    private void Clear(int _)
    {
        BaseMachine[] allMachines = FindObjectsByType<BaseMachine>(FindObjectsSortMode.None);
        foreach(BaseMachine machine in allMachines)
        {
            Destroy(machine.gameObject);
        }

        ConveyorItem[] allItems = FindObjectsByType<ConveyorItem>(FindObjectsSortMode.None);
        foreach(ConveyorItem item in allItems)
        {
            Destroy(item.gameObject);
        }

        Inventory.Current.ClearInventory();
    }
}
