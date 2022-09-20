using System.Collections.Generic;
using UnityEngine;

public class CreatePoints : MonoBehaviour
{
    [SerializeField] private float _radiusHair;

    private float _radius;
    private Vector3 _originPosition;
    private float _possibleCount;

    public List<Vector3> FindPlaceWithCircle()
    {
        _radius = gameObject.transform.localScale.z;
        _originPosition = transform.position;
        List<Vector3> _position = new List<Vector3>();
        Vector3 point = _originPosition;
        float distanceRing = Mathf.PI * 2 * _radius;
        var angle = 360 * Mathf.Deg2Rad;
        var ringCount = (int)(distanceRing / (Mathf.PI * 2 * _radiusHair));

        for (int j = ringCount; j > 0; j--)
        {
            _possibleCount = (int)(distanceRing / _radiusHair);

            for (int i = 0; i <= _possibleCount; i++)
            {
                float z = _originPosition.z + Mathf.Cos(angle / _possibleCount * i) * _radius;
                float x = _originPosition.x + Mathf.Sin(angle / _possibleCount * i) * _radius;
                point.x = x;
                point.z = z;

                _position.Add(point);
            }

            _radius -= _radiusHair;
            distanceRing = Mathf.PI * 2 * _radius;
        }

        return _position;
    }
}