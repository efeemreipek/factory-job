using UnityEngine;

public class Supplier : BaseMachine
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private float spawnInterval = 2f;

    private float spawnTimer = 0f;
    private bool isActive = false;

    public bool IsActive { 
        get => isActive; 
        set
        {
            isActive = value;
            spawnTimer = spawnInterval;
        }
    }

    private void Update()
    {
        if(!isPlaced) return;

        if(CanAcceptItem())
        {
            spawnTimer += Time.deltaTime;
            if(spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f;
                SpawnItem();
            }
        }
    }

    private void SpawnItem()
    {
        if(itemPrefab == null) return;
        if(!isActive) return;

        GameObject itemGO = Instantiate(itemPrefab, itemTargetPoint.position, Quaternion.identity);
        ConveyorItem item = itemGO.GetComponent<ConveyorItem>();

        if(item != null)
        {
            AcceptItem(item);
        }
    }
}
