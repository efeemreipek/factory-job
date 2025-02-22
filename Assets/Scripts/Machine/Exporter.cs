using UnityEngine;

public class Exporter : BaseMachine
{
    public override void AcceptItem(ConveyorItem item)
    {
        if(CanAcceptItem())
        {
            currentItem = item;
            item.SetCurrentMachine(this);
            Inventory.Current.AddToInventory(currentItem.ItemData);
            Destroy(currentItem.gameObject);
            currentItem = null;
        }
    }
}
