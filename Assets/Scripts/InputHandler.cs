using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputHandler : Singleton<InputHandler>
{
    private InputAction actionMousePosition;
    private InputAction actionMouseLeftClick;
    private InputAction actionMouseRightClick;
    private InputAction actionRotate;
    private InputAction actionMove;

    private Vector2 mousePosition;
    private Vector2 move;

    private bool mouseLeftClick;
    private bool mouseRightClick;
    private bool rotate;

    private bool mouseLeftClickDown;
    private bool mouseRightClickDown;
    private bool rotateDown;

    private bool mouseLeftClickUp;
    private bool mouseRightClickUp;
    private bool rotateUp;

    private bool isMouseOverUI;

    public Vector2 MousePosition => mousePosition;
    public Vector2 Move => move;
    public bool MouseLeftClick => mouseLeftClick;
    public bool MouseRightClick => mouseRightClick;
    public bool Rotate => rotate;

    public bool MouseLeftClickDown => mouseLeftClickDown;
    public bool MouseRightClickDown => mouseRightClickDown;
    public bool RotateDown => rotateDown;

    public bool MouseLeftClickUp => mouseLeftClickUp;
    public bool MouseRightClickUp => mouseRightClickUp;
    public bool RotateUp => rotateUp;

    public bool IsMouseOverUI => isMouseOverUI;

    private void OnEnable()
    {
        actionMousePosition = InputSystem.actions.FindAction("Mouse Position");
        actionMouseLeftClick = InputSystem.actions.FindAction("Mouse Left Click");
        actionMouseRightClick = InputSystem.actions.FindAction("Mouse Right Click");
        actionRotate = InputSystem.actions.FindAction("Rotate");
        actionMove = InputSystem.actions.FindAction("Move");

        actionMousePosition.performed += MousePosition_Performed;
        actionMousePosition.canceled += MousePosition_Canceled;

        actionMouseLeftClick.performed += MouseLeftClick_Performed;
        actionMouseLeftClick.canceled += MouseLeftClick_Canceled;

        actionMouseRightClick.performed += MouseRightClick_Performed;
        actionMouseRightClick.canceled += MouseRightClick_Canceled;

        actionRotate.performed += Rotate_Performed;
        actionRotate.canceled += Rotate_Canceled;

        actionMove.performed += Move_Performed;
        actionMove.canceled += Move_Canceled;

        InputSystem.actions.Enable();
    }
    private void OnDisable()
    {
        actionMousePosition.performed -= MousePosition_Performed;
        actionMousePosition.canceled -= MousePosition_Canceled;

        actionMouseLeftClick.performed -= MouseLeftClick_Performed;
        actionMouseLeftClick.canceled -= MouseLeftClick_Canceled;

        actionMouseRightClick.performed -= MouseRightClick_Performed;
        actionMouseRightClick.canceled -= MouseRightClick_Canceled;

        actionRotate.performed -= Rotate_Performed;
        actionRotate.canceled -= Rotate_Canceled;

        actionMove.performed -= Move_Performed;
        actionMove.canceled -= Move_Canceled;

        InputSystem.actions.Disable();
    }
    private void Update()
    {
        isMouseOverUI = EventSystem.current.IsPointerOverGameObject();
    }
    private void LateUpdate()
    {
        mouseLeftClickDown = false;
        mouseRightClickDown = false;
        rotateDown = false;

        mouseLeftClickUp = false;
        mouseRightClickUp = false;
        rotateUp = false;
    }

    private void MousePosition_Performed(InputAction.CallbackContext obj) => mousePosition = obj.ReadValue<Vector2>();
    private void MousePosition_Canceled(InputAction.CallbackContext obj) => mousePosition = Vector2.zero;
    private void MouseLeftClick_Performed(InputAction.CallbackContext obj)
    {
        mouseLeftClick = true;
        mouseLeftClickDown = true;
    }
    private void MouseLeftClick_Canceled(InputAction.CallbackContext obj)
    {
        mouseLeftClick = false;
        mouseLeftClickUp = true;
    }
    private void MouseRightClick_Performed(InputAction.CallbackContext obj)
    {
        mouseRightClick = true;
        mouseRightClickDown = true;
    }
    private void MouseRightClick_Canceled(InputAction.CallbackContext obj)
    {
        mouseRightClick = false;
        mouseRightClickUp = true;
    }
    private void Rotate_Performed(InputAction.CallbackContext obj)
    {
        rotate = true;
        rotateDown = true;
    }
    private void Rotate_Canceled(InputAction.CallbackContext obj)
    {
        rotate = false;
        rotateUp = true;
    }
    private void Move_Performed(InputAction.CallbackContext obj) => move = obj.ReadValue<Vector2>();
    private void Move_Canceled(InputAction.CallbackContext obj) => move = Vector2.zero;
}
