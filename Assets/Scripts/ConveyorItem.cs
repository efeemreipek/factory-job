using DG.Tweening;
using UnityEngine;

public class ConveyorItem : MonoBehaviour
{
    [SerializeField] private GameObject visualGameObject;
    [SerializeField] private ItemData itemData;

    private BaseMachine currentMachine;

    public ItemData ItemData {  
        get { return itemData; }
        set
        {
            itemData = value;
            UpdateAppearance();
        }
    }

    public void SetCurrentMachine(BaseMachine machine)
    {
        currentMachine = machine;
    }

    private void UpdateAppearance()
    {
        if(itemData != null && itemData.VisualPrefab != null)
        {
            foreach(Transform child in visualGameObject.transform)
            {
                Destroy(child.gameObject);
            }
            Instantiate(itemData.VisualPrefab, visualGameObject.transform);
        }
    }
}
