using UnityEngine;

public class ConveyorBelt : BaseMachine
{
    [SerializeField] private MeshRenderer beltMeshRenderer;

    private void Update()
    {
        if(!isPlaced) return;

        beltMeshRenderer.material.mainTextureOffset += new Vector2(0, 1) * moveData.MoveDuration * Time.deltaTime;
        if(beltMeshRenderer.material.mainTextureOffset.y >= 1)
        {
            beltMeshRenderer.material.mainTextureOffset = Vector2.zero;
        }
    }

    protected override void TryMoveItem()
    {
        if(currentItem != null && nextMachine != null && nextMachine.CanAcceptItem())
        {
            MoveItemToNext();
        }
    }
}
