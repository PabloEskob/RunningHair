using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(SplineFollower))]
public class CameraMover : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;

    private SplineFollower _splineFollower;

    private void Start()
    {
        _splineFollower = GetComponent<SplineFollower>();
        Move();
    }

    private void Move()
    {
        _splineFollower.followSpeed = _player.SpeedForward;
    }
}