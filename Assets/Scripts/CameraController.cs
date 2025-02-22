using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private Vector2 xBounds;
    [SerializeField] private Vector2 zBounds;


    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void LateUpdate()
    {
        Vector3 inputDirection = new Vector3(InputHandler.Instance.Move.x, 0f, InputHandler.Instance.Move.y);
        targetPosition += inputDirection * moveSpeed * Time.deltaTime;

        targetPosition.x = Mathf.Clamp(targetPosition.x, xBounds.x, xBounds.y);
        targetPosition.z = Mathf.Clamp(targetPosition.z, zBounds.x, zBounds.y);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
