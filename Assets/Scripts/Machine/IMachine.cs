using UnityEngine;

public interface IMachine
{
    void ConfirmPlacement();
    bool CanAcceptItem();
    void AcceptItem(ConveyorItem item);
    Transform GetItemTargetPoint();
}
