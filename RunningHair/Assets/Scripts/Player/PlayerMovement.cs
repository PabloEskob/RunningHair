using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotate;
    [SerializeField] private SplineFollower _spline;
    [SerializeField] private float _limitation;
    [SerializeField] private SplineFollower _cameraSpline;
    [SerializeField] private float _flySpeed;
    
    private PlayerInput _playerInput;
    private bool isMove=true;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        AllowMoveAndRotate();
    }

    public void Move(float direction)
    {
        var offset = direction * _speed * Time.deltaTime;
        var motionOffset = _spline.motion.offset;
        motionOffset.x += offset;
        motionOffset.x = Mathf.Clamp(motionOffset.x, -_limitation, _limitation);
        _spline.motion.offset = motionOffset;
    }

    public void BanMoveAndRotate()
    {
        isMove = false;
    }

    public void StartFly()
    {
        _spline.followSpeed = _flySpeed;
        _cameraSpline.followSpeed = _spline.followSpeed;
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
            Move( _playerInput.Movement);
            Rotate( _playerInput.Movement);
        }
    }
}