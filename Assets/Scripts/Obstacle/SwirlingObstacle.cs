using UnityEngine;

public abstract class SwirlingObstacle : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _sideSwirling;

    public void StartSwirling()
    {
        gameObject.transform.Rotate(_sideSwirling * _speed * Time.deltaTime);
    }
}