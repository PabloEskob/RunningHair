using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairCycle : MonoBehaviour
{
    [SerializeField] private Hairstyle _hairstyle;
    [SerializeField] private HairMovement _player;
    [SerializeField] private float _radius;
    [SerializeField] private int _countPoint;
    [SerializeField] private CyclePoint _point;
    [SerializeField] private float _speed;
    [SerializeField] private float _positionY;
    [SerializeField] private float _speedSwirlPool;
    [SerializeField] private float _newRadius;

    private List<CyclePoint> _cyclePoints;
    private IEnumerator _coroutine;

    private void Update()
    {
        if (_cyclePoints != null)
        {
            for (int i = 0; i < _cyclePoints.Count; i++)
            {
                int angleStep = 360 / _countPoint * i;
                Vector3 position = RandomCircle(SetStartPosition(), _newRadius, angleStep);
                _cyclePoints[i].IncreaseRadius(position, _speed, _positionY);
               _cyclePoints[i].SetWirlPool(SetStartPosition(), _speedSwirlPool);
            }
        }
    }

    public void CreateCirclePoints()
    {
        _cyclePoints = new List<CyclePoint>();
        Vector3 originPosition = SetStartPosition();

        for (int i = 0; i < _countPoint; i++)
        {
            int angleStep = 360 / _countPoint * i;
            Vector3 position = RandomCircle(originPosition, _radius, angleStep);
            var game = Instantiate(_point, position, Quaternion.identity, _player.transform);
            _cyclePoints.Add(game);
            game.transform.parent = _hairstyle.transform;
        }

        AddHair();
    }

    private void AddHair()
    {
        for (int i = 0; i < _hairstyle.FindLastHair().Count; i++)
        {
            _cyclePoints[i].Join(_hairstyle.FindLastHair()[i].GetComponent<Rigidbody>());
        }
    }


    Vector3 RandomCircle(Vector3 center, float radius, int angleStep)
    {
        Vector3 position;
        position.x = center.x + radius * Mathf.Sin(angleStep * Mathf.Deg2Rad);
        position.z = center.z + radius * Mathf.Cos(angleStep * Mathf.Deg2Rad);
        position.y = center.y;
        return position;
    }

    private float SetStartPositionY()
    {
        float positionY = 0;

        for (int i = 0; i < 1; i++)
        {
            positionY = _hairstyle.FindLastHair()[i].transform.position.y + _positionY;
        }

        return positionY;
    }

    private Vector3 SetStartPosition()
    {
        return transform.position =
            new Vector3(transform.position.x, SetStartPositionY(), transform.position.z);
    }
}