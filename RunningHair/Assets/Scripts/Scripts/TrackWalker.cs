using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackWalker : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Rigidbody _rigidbody;

    public Vector3 ForwardDirection { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();    
    }

    private void FixedUpdate()
    {
        SetForwardDirection();
        Rotate();
        MoveAlongTrack();
    }

    public void MoveAlongTrack()
    {
        _rigidbody.velocity = ForwardDirection * _player.Speed;
    }

    public void SetForwardDirection()
    {
        if (Physics.Raycast(transform.position, transform.right, out RaycastHit hit, 20))
            ForwardDirection = Vector3.Cross(hit.normal, -Vector3.up);
    }

    public void Rotate()
    {
        _rigidbody.rotation = Quaternion.LookRotation(ForwardDirection);
    }
}
