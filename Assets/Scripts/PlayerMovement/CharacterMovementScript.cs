using UnityEngine;
using Pixelplacement;

public class CharacterMovementScript : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    [SerializeField] private CharacterController _controller;

    [SerializeField] private Transform _cameraTransform;

    private InputManager _inputManager;

    [Space]
    [SerializeField] private GameObject _playerModel;
    [SerializeField] private float _modelTurnRate;

    [Space]
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravityMultiplier;
    private float _velocityY;
    private bool _jumped;

    private void OnEnable()
    {
        InputManager.OnJumpPerformed += Jump;
    }

    void Start()
    {
        _controller = GetComponent<CharacterController>();

        _inputManager = InputManager.Instance;

        _cameraTransform = CustomGameManager.Instance.CameraTransform.GetChild(0).transform;
    }

    private void Update()
    {

        Vector2 input = _inputManager.GetPlayerMovementVector();

        Vector3 movementDirection = ApplyCameraRotation(new Vector3(input.x, 0f, input.y));

        Vector3 movementVector = Vector3.up * _velocityY * Time.deltaTime;

        _controller.Move(movementVector + movementDirection * Time.deltaTime * _movementSpeed);

        Vector2 modelRotation = new Vector2(movementDirection.x, movementDirection.z);

        RotateModel(modelRotation);
        
    }

    private void FixedUpdate()
    {
        ManageGravity();
    }

    private void OnDestroy()
    {
        InputManager.OnJumpPerformed -= Jump;
    }

    private void RotateModel(Vector2 input)
    {

        if (input.x == 0 && input.y == 0)
            return;

        Vector2 normalized = input.normalized;

        Vector3 targetRotation = Vector3.up * Mathf.Atan2(normalized.x, normalized.y) * Mathf.Rad2Deg;
        _playerModel.transform.rotation = Quaternion.Lerp(_playerModel.transform.rotation, Quaternion.Euler(targetRotation), _modelTurnRate * Time.deltaTime);
    }

    private void ManageGravity()
    {

        _velocityY += Time.deltaTime * (_gravityMultiplier * _gravityMultiplier * -1f);

        if (_controller.isGrounded)
        {
            _velocityY = 0f;
            _jumped = false;
        }

    }

    private Vector3 ApplyCameraRotation(Vector3 vectorToRotate)
    {
        float currentY = vectorToRotate.y;

        Vector3 camForward = _cameraTransform.forward;
        Vector3 camRight = _cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward = camForward.normalized;
        camRight = camRight.normalized;

        Vector3 cameraForwardZProduct = vectorToRotate.z * camForward;
        Vector3 cameraRightXProduct = vectorToRotate.x * camRight;

        Vector3 vectorRotated = cameraForwardZProduct + cameraRightXProduct;
        vectorRotated.y = currentY;

        return vectorRotated;
    }

    private void Jump()
    {
        if ((_controller.isGrounded || _velocityY <= 0f) && !_jumped)
        {
            _velocityY += 10f * _jumpHeight;
            _jumped = true;
        }
    }
}
