using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _speedForward;
    [SerializeField] private float _speedSide;
    [SerializeField] private float _speedRotate;
    [SerializeField] private SplineFollower _spline;
    [SerializeField] private float _limitation;
    [SerializeField] private float _flySpeed;

    private PlayerInput _playerInput;
    private bool isMove = true;

    public float SpeedForward => _speedForward;

    private void Start()
    {
        _spline.followSpeed = _speedForward;
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        AllowMoveAndRotate();
    }

    public void Move(float direction)
    {
        var offset = direction * _speedSide * Time.deltaTime;
        var motionOffset = _spline.motion.offset;
        motionOffset.x += offset;
        motionOffset.x = Mathf.Clamp(motionOffset.x, -_limitation, _limitation);
        _spline.motion.offset = motionOffset;
    }

    public void BanMoveAndRotate()
    {
        isMove = false;
    }

    public void StartFly(SplineFollower splineFollower)
    {
        splineFollower.followSpeed = _flySpeed;
    }
    
    private void Rotate(float axis)
    {
        var offset = axis * _speedRotate * Time.deltaTime;
        var motionOffset = _spline.motion.rotationOffset;
        motionOffset.y = offset;
        _spline.motion.rotationOffset = motionOffset;
    }

    private void AllowMoveAndRotate()
    {
        if (isMove)
        {
            Move(_playerInput.Movement);
            Rotate(_playerInput.Movement);
        }
    }
}