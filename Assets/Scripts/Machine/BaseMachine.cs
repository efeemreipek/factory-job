using DG.Tweening;
using UnityEngine;

public abstract class BaseMachine : MonoBehaviour, IMachine
{
    [SerializeField] protected MachineMoveData moveData;
    [SerializeField] protected Transform itemTargetPoint;
    [SerializeField] protected GameObject arrowGameObject;

    protected ConveyorItem currentItem = null;
    protected BaseMachine nextMachine = null;
    protected float moveTimer = 0f;
    protected Vector3 direction;
    protected bool isPlaced = false;
    protected bool isMoving = false;

    public BaseMachine NextMachine => nextMachine;
    public ConveyorItem CurrentItem => currentItem;
    public bool IsPlaced => isPlaced;

    protected virtual void Start()
    {
        ConveyorTimingManager.Current.Initialize(moveData.MoveInterval);
        ConveyorTimingManager.Current.RegisterMachine(this);
    }
    protected virtual void OnDestroy()
    {
        if(currentItem != null)
        {
            DOTween.Kill(currentItem.transform);
        }

        ConveyorTimingManager.Current.UnregisterMachine(this);
    }

    public virtual void OnGlobalTick()
    {
        if(!isPlaced || isMoving) return;

        TryFindNextMachine();
        ProcessItem();
        TryMoveItem();
    }

    public virtual void ConfirmPlacement()
    {
        isPlaced = true;
        AudioManager.Current.PlaySound(AudioClips.MachinePlace_SFX, transform.position, 0.15f);
        direction = transform.forward;
        arrowGameObject.SetActive(false);
        TryFindNextMachine();
    }
    public virtual bool CanAcceptItem() => currentItem == null && !isMoving;
    public virtual void AcceptItem(ConveyorItem item)
    {
        if(CanAcceptItem())
        {
            currentItem = item;
            item.SetCurrentMachine(this);
        }
    }
    public Transform GetItemTargetPoint() => itemTargetPoint;

    protected virtual void ProcessItem()
    {
        // empty for specific machines
    }
    protected virtual void TryMoveItem()
    {
        if(currentItem != null && nextMachine != null && nextMachine.CanAcceptItem())
        {
            if(nextMachine is not ConveyorBelt)
            {
                return;
            }

            MoveItemToNext();
        }
    }
    protected void MoveItemToNext()
    {
        isMoving = true;

        Vector3 targetPosition = nextMachine.GetItemTargetPoint().position;

        currentItem.transform
            .DOMove(targetPosition, moveData.MoveDuration)
            .SetEase(moveData.MoveEase)
            .OnComplete(() =>
            {
                nextMachine.AcceptItem(currentItem);
                currentItem = null;
                isMoving = false;
            });
    }
    protected virtual void TryFindNextMachine()
    {
        if(nextMachine != null) return;

        RaycastHit hit;
        float rayOriginOffset = 0.2f;
        float rayLength = 1f;

        if(Physics.Raycast(transform.position + Vector3.up * rayOriginOffset, direction, out hit, rayLength))
        {
            BaseMachine potentialMachine = hit.collider.gameObject.GetComponentInParent<BaseMachine>();

            if(potentialMachine != null && potentialMachine.isPlaced)
            {
                nextMachine = potentialMachine;
            }
        }
    }
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * 0.2f, direction);
    }
}
