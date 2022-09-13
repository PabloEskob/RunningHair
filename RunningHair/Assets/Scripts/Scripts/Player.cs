using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(PlayerInput))][RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float _sideSpeed;
    [SerializeField] private TrackWalker _trackWalker;

    private PlayerInput _playerInput;
    private Vector3 _viewDirection;
    private Rigidbody _rigidbody;
    public float Speed => _forwardSpeed;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        float yAxisPlayerOffset = 0.1f;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + yAxisPlayerOffset, transform.position.z);
        }

        Vector3 leftRightVelocity = Vector3.Cross(Vector3.up, _trackWalker.ForwardDirection) * _playerInput.Movement;
        Vector3 forwardVelociity = _trackWalker.ForwardDirection * _forwardSpeed;
        _rigidbody.velocity = forwardVelociity + leftRightVelocity;
    }

    private void Rotate()
    {
        float deflectionMultiplier = 2;

        if (_playerInput.Movement > 0)
            _viewDirection = _trackWalker.transform.TransformDirection(Vector3.forward + Vector3.right * deflectionMultiplier);
        else if (_playerInput.Movement < 0)
            _viewDirection = _trackWalker.transform.TransformDirection(Vector3.forward + Vector3.left * deflectionMultiplier);
        else
            _viewDirection = _trackWalker.transform.TransformDirection(Vector3.zero);

        _viewDirection += _trackWalker.ForwardDirection;

        GetComponent<Rigidbody>().rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_viewDirection), Time.deltaTime * 30);
    }
}
