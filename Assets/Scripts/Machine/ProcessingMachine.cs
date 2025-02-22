using UnityEngine;

public class ProcessingMachine : BaseMachine
{
    [SerializeField] private ItemData inputItem;
    [SerializeField] private ItemData outputItem;

    protected override void ProcessItem()
    {
        if(currentItem != null && currentItem.ItemData == inputItem)
        {
            currentItem.ItemData = outputItem;
        }
    }
}
