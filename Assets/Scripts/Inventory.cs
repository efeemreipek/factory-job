using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public event Action<int, int> OnInventoryChanged; // index, amount

    [SerializeField] private List<ItemData> itemDataList = new List<ItemData>();

    private Dictionary<ItemData, int> inventory;

    private void Start()
    {
        inventory = new Dictionary<ItemData, int>
        {
            { itemDataList[0], 0 }, // Raw Iron
            { itemDataList[1], 0 }, // Iron Ingot
            { itemDataList[2], 0 }, // Iron Plate
            { itemDataList[3], 0 }  // Iron Rod
        };
    }

    public void AddToInventory(ItemData itemData)
    {
        inventory[itemData]++;
        int itemIndex = itemDataList.IndexOf(itemData);
        OnInventoryChanged?.Invoke(itemIndex, inventory[itemData]);
    }

    public void RemoveFromInventory(ItemData itemData)
    {
        inventory[itemData]--;
        if(inventory[itemData] < 0) inventory[itemData] = 0;
        int itemIndex = itemDataList.IndexOf(itemData);
        OnInventoryChanged?.Invoke(itemIndex, inventory[itemData]);
    }
    public int CheckItemAmount(int index)
    {
        return inventory[itemDataList[index]];
    } 
    public void ClearInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            inventory[itemDataList[i]] = 0;
        }
    }
}
