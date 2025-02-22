using UnityEngine;
using DG.Tweening;

public class MachinePlacer : MonoBehaviour
{
    private MachineSelector machineSelector;
    private GameObject currentMachine;
    private GameObject selectedPrefab;

    private int currentRotationIndex = 0;

    private void Awake()
    {
        machineSelector = GetComponent<MachineSelector>();
    }
    private void OnEnable()
    {
        machineSelector.OnMachineSelected += MachineSelector_OnMachineSelected;
    }
    private void OnDisable()
    {
        machineSelector.OnMachineSelected -= MachineSelector_OnMachineSelected;
    }

    private void Update()
    {
        if(selectedPrefab != null && currentMachine == null)
        {
            currentMachine = Instantiate(selectedPrefab);
            Vector3 targetRotationEuler = new Vector3(0f, currentRotationIndex * 90f, 0f);
            currentMachine.transform.rotation = Quaternion.Euler(targetRotationEuler);
        }

        if(currentMachine == null) return;

        // move the preview machine
        Vector2Int gridPos = GetMouseGridPosition();
        currentMachine.transform.position = new Vector3(gridPos.x, currentMachine.transform.position.y, gridPos.y);

        // rotate the preview machine
        if(InputHandler.Instance.RotateDown)
        {
            DOTween.Kill(currentMachine.transform);

            currentRotationIndex = (currentRotationIndex + 1) % 4;
            float targetRotation = currentRotationIndex * 90f;
            Vector3 targetRotationEuler = new Vector3(0f, targetRotation, 0f);

            currentMachine.transform.DOLocalRotate(targetRotationEuler, 0.25f).SetEase(Ease.OutQuad);
        }

        // place the preview machine
        if(InputHandler.Instance.MouseLeftClickDown && !InputHandler.Instance.IsMouseOverUI)
        {
            if(DOTween.IsTweening(currentMachine.transform))
            {
                DOTween.Complete(currentMachine.transform);
            }

            Vector3 checkGridPos = new Vector3(gridPos.x, 0.25f, gridPos.y);
            Collider[] colliders = Physics.OverlapBox(checkGridPos, Vector3.one * 0.25f);

            bool isOccupied = false;
            BaseMachine existingMachine = null;

            foreach(Collider collider in colliders)
            {
                BaseMachine machine = collider.GetComponentInParent<BaseMachine>();

                if(machine != null && machine != currentMachine.GetComponent<BaseMachine>())
                {
                    isOccupied = true;
                    existingMachine = machine;
                    break;
                }
            }

            if(isOccupied) return;

            currentMachine.GetComponent<BaseMachine>().ConfirmPlacement();
            currentMachine = null;
        }
    }

    private void MachineSelector_OnMachineSelected(GameObject machinePrefab)
    {
        if(currentMachine != null)
        {
            Destroy(currentMachine);
            currentMachine = null;
        }

        selectedPrefab = machinePrefab;
        currentRotationIndex = 0;

        if(machinePrefab == null)
        {
            selectedPrefab = null;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputHandler.Instance.MousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Ground")))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
    private Vector2Int GetMouseGridPosition()
    {
        Vector3 worldPos = GetMouseWorldPosition();
        return new Vector2Int(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.z));
    }
}
