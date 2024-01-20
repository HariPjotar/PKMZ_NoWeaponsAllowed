using UnityEngine;

public class CharacterMovementScript : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    [SerializeField] private CharacterController _controller;

    private InputManager _inputManager;

    void Start()
    {
        _controller = GetComponent<CharacterController>();

        _inputManager = InputManager.Instance;
    }

    private void Update()
    {
        Vector2 input = _inputManager.GetPlayerMovementVector();
        Vector3 movementDirection = new Vector3(input.x, 0f, input.y);

        _controller.Move(movementDirection * Time.deltaTime * _movementSpeed);
    }
}
