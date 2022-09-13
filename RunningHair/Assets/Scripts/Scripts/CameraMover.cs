using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _target;
    [SerializeField] private float _yOffset;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _zOffset;
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        Vector3 targetPosition = _player.position + _player.transform.TransformDirection(new Vector3(_xOffset, _yOffset, _zOffset));
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _speed);
        transform.LookAt(_target);
    }
}