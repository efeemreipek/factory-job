using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> itemAmountTextList = new List<TextMeshProUGUI>();

    private int[] requirements;

    private void Start()
    {
        Inventory.Current.OnInventoryChanged += Inventory_OnInventoryChanged;
        ContractManager.Current.OnContractPicked += ContractManager_OnContractPicked;
    }
    private void OnDestroy()
    {
        Inventory.Current.OnInventoryChanged -= Inventory_OnInventoryChanged;
        ContractManager.Current.OnContractPicked -= ContractManager_OnContractPicked;
    }

    private void Inventory_OnInventoryChanged(int index, int amount)
    {
        itemAmountTextList[index].text = $"{amount.ToString()}/{requirements[index]}";
    }
    private void ContractManager_OnContractPicked(int[] requirements)
    {
        this.requirements = requirements;

        for(int i = 0; i < requirements.Length; i++)
        {
            itemAmountTextList[i].text = $"0/{requirements[i]}";
        }
    }
}
