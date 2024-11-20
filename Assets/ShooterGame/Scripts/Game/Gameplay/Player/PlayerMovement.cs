using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private readonly float _playerHeight = 2f;

    [SerializeField] Transform orientation;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float airMultiplier = 0.4f;
    private readonly float _movementMultiplier = 10f;

    [Header("Sprinting")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float acceleration = 10f;
    
    [Header("Jumping")]
    [SerializeField] private float jumpForce = 5f;

    [Header("Keybindings")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Drag")]
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 2f;

    private float _horizontalMovement;
    private float _verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.2f;
    public bool IsGrounded { get; private set; }

    private Vector3 _moveDirection;
    private Vector3 _slopeMoveDirection;

    private Rigidbody _rigidbody;

    private RaycastHit _slopeHit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _playerHeight / 2 + 0.5f))
        {
            if (_slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    private void Update()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();
        ControlSpeed();

        if (Input.GetKeyDown(jumpKey) && IsGrounded)
        {
            Jump();
        }

        _slopeMoveDirection = Vector3.ProjectOnPlane(_moveDirection, _slopeHit.normal);
    }

    void MyInput()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _verticalMovement = Input.GetAxisRaw("Vertical");

        _moveDirection = orientation.forward * _verticalMovement + orientation.right * _horizontalMovement;
    }

    void Jump()
    {
        if (IsGrounded)
        {
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
            _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    void ControlSpeed()
    {
        if (Input.GetKey(sprintKey) && IsGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void ControlDrag()
    {
        if (IsGrounded)
        {
            _rigidbody.linearDamping = groundDrag;
        }
        else
        {
            _rigidbody.linearDamping = airDrag;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (IsGrounded && !OnSlope())
        {
            _rigidbody.AddForce(_moveDirection.normalized * moveSpeed * _movementMultiplier, ForceMode.Acceleration);
        }
        else if (IsGrounded && OnSlope())
        {
            _rigidbody.AddForce(_slopeMoveDirection.normalized * moveSpeed * _movementMultiplier, ForceMode.Acceleration);
        }
        else if (!IsGrounded)
        {
            _rigidbody.AddForce(_moveDirection.normalized * moveSpeed * _movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }
}