using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    public static event Action OnJumpPerformed;

    private static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private PlayerInputActions _inputActions;

    private Vector2 _movementVector;

    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        _inputActions = new PlayerInputActions();

        _inputActions.DefaultActionMap.Enable();
    }

    private void OnDisable()
    {
        _inputActions.DefaultActionMap.Disable();
    }

    public Vector2 GetPlayerMovementVector()
    {
        return _inputActions.DefaultActionMap.Movement.ReadValue<Vector2>();
    }

    public void OnJumpKeyPress(InputAction.CallbackContext Action)
    {
        if(Action.started)
        {
            OnJumpPerformed?.Invoke();
        }
    }
}
