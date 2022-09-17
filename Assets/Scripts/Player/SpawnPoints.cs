using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private Hair _hair;

    private float _radius;
    private float _radiusHair;
    private List<Vector3> _position = new List<Vector3>();
    private Vector3 _originPosition;
    private float _possibleCount;

    private void Start()
    {
        _radius = gameObject.transform.localScale.z;
        _radiusHair = _hair.GetComponent<SkinnedMeshRenderer>().bounds.extents.x;
        _originPosition = transform.position;
        FindPlaceWithCircle();
        indsa();
    }

    private void FindPlaceWithCircle()
    {
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
    }

    private void indsa()
    {
        foreach (var VARIABLE in _position)
        {
          var ds=  Instantiate(_hair, VARIABLE, Quaternion.identity);
          ds.transform.parent = gameObject.transform;
        }
    }
}