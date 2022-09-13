using System;
using UnityEngine;

public class AnimationObstacles : MonoBehaviour
{
    [SerializeField] private Roller[] _rollers;
    [SerializeField] private Saw[] _saws;
    [SerializeField] private MovingSaw _movingSaw;

    private void Start()
    {
        _movingSaw.StartMove();
    }

    private void Update()
    {
        Swirling(_rollers);
        Swirling(_saws);
    }

    private void Swirling(SwirlingObstacle[] swirlingObstacles)
    {
        foreach (var swirlingObstacle in swirlingObstacles)
        {
            swirlingObstacle.StartSwirling();
        }
    }
}