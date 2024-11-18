using UnityEngine;
using UnityEngine.Serialization;

public class WallRun : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform orientation;

    [Header("Detection")]
    [SerializeField] private float wallDistance = .5f;
    [SerializeField] private float minimumJumpHeight = 1.5f;

    [Header("Wall Running")]
    [SerializeField] private float wallRunGravity;
    [SerializeField] private float wallRunJumpForce;

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float fov;
    [SerializeField] private float wallRunFOV;
    [SerializeField] private float wallRunFOVTime;
    [SerializeField] private float camTilt;
    [SerializeField] private float camTiltTime;

    public float Tilt { get; private set; }

    private bool _wallLeft = false;
    private bool _wallRight = false;

    private RaycastHit _leftWallHit;
    private RaycastHit _rightWallHit;

    private Rigidbody _rigidbody;

    private bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void CheckWall()
    {
        _wallLeft = Physics.Raycast(transform.position, -orientation.right, out _leftWallHit, wallDistance);
        _wallRight = Physics.Raycast(transform.position, orientation.right, out _rightWallHit, wallDistance);
    }

    private void Update()
    {
        CheckWall();

        if (CanWallRun())
        {
            if (_wallLeft)
            {
                StartWallRun();
                Debug.Log("wall running on the left");
            }
            else if (_wallRight)
            {
                StartWallRun();
                Debug.Log("wall running on the right");
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    private void StartWallRun()
    {
        _rigidbody.useGravity = false;

        _rigidbody.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunFOV, wallRunFOVTime * Time.deltaTime);

        if (_wallLeft)
            Tilt = Mathf.Lerp(Tilt, -camTilt, camTiltTime * Time.deltaTime);
        else if (_wallRight)
            Tilt = Mathf.Lerp(Tilt, camTilt, camTiltTime * Time.deltaTime);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_wallLeft)
            {
                Vector3 wallRunJumpDirection = transform.up + _leftWallHit.normal;
                _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
                _rigidbody.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
            }
            else if (_wallRight)
            {
                Vector3 wallRunJumpDirection = transform.up + _rightWallHit.normal;
                _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z); 
                _rigidbody.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
            }
        }
    }

    void StopWallRun()
    {
        _rigidbody.useGravity = true;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunFOVTime * Time.deltaTime);
        Tilt = Mathf.Lerp(Tilt, 0, camTiltTime * Time.deltaTime);
    }
}