using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Hair : MonoBehaviour
{
    [SerializeField] private List<Transform> _startGrowthPoints;
    [SerializeField] private Transform _growPoints;
    [SerializeField] private Volos _volos;
    [SerializeField] private Material _upperGreenGradient;
    [SerializeField] private Material _upperRedGradient;
    [SerializeField] private Material _green;
    [SerializeField] private Material _red;

    private Volos[,] _hair;
    private int _maxLenght = 20;

    public int CurrentLenght { get; private set; }

    private void Awake()
    {
        _hair = new Volos[_maxLenght, _startGrowthPoints.Count];
    }

    private void Start()
    {
        int startLenght = 2;

        for (int i = 0; i < startLenght; i++)
        {
            List<Transform> growPoints = new List<Transform>();

            for (int j = 0; j < _startGrowthPoints.Count; j++)
            {
                if (i == 0)
                    growPoints.Add(_startGrowthPoints[j]);
                else
                    growPoints.Add(_hair[CurrentLenght, j].VolosEnd);

                Volos volos = Instantiate(_volos, growPoints[j].position, GetRandomRotation(), transform);
                volos.SetSegmentNumber(CurrentLenght);
                volos.SetPositionNumber(j);

                if (i == startLenght - 1)
                    volos.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);

                _hair[i, j] = volos;

                if (i == 0)
                    volos.Init(_startGrowthPoints[j]);
                else
                    volos.Init(_hair[CurrentLenght, j].VolosEnd);
            }

            CurrentLenght = i;
        }

        for (int j = 0; j < _startGrowthPoints.Count; j++)
        {
            _hair[0, j].GetComponent<ConfigurableJoint>().connectedBody = _startGrowthPoints[j].GetComponent<Rigidbody>();
        }

        for (int j = 0; j < _startGrowthPoints.Count; j++)
        {
            _hair[1, j].GetComponent<ConfigurableJoint>().connectedBody = _hair[0, j].VolosEnd.GetComponent<Rigidbody>();
        }
    }

    private Quaternion GetRandomRotation()
    {
        var random = (min: -10, max: 10);

        float tilt = Random.Range(random.min, random.max);
        return Quaternion.Euler(tilt, 0, tilt);
    }

    public void Grow()
    {
        for (int j = 0; j < _startGrowthPoints.Count; j++)
        {
            Vector3 growthPoint = _hair[CurrentLenght, j].VolosEnd.position;

            Volos currentVolos = Instantiate(_volos, growthPoint, GetRandomRotation(), transform);

            _hair[CurrentLenght, j].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100);
            currentVolos.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);

            _hair[CurrentLenght + 1, j] = currentVolos;

            currentVolos.SetSegmentNumber(CurrentLenght + 1);
            currentVolos.SetPositionNumber(j);

            currentVolos.Init(_hair[CurrentLenght, j].VolosEnd);

            if (_hair[CurrentLenght, j].IsColored == true)
            {
                Color previosVolosColor = _hair[CurrentLenght, j].GetComponent<SkinnedMeshRenderer>().material.color;

                if (previosVolosColor == _green.color)
                    currentVolos.Colorise(_upperGreenGradient);
                else if (previosVolosColor == _red.color)
                    currentVolos.Colorise(_upperRedGradient);
            }
        }

        CurrentLenght++;

        BindJointBody();
    }

    public Volos GetUpperSegment(Volos volos)
    {
        return _hair[volos.SegmentNumber + 1, volos.PositionNumber];
    }

    public Volos GetLowerSegment(Volos volos)
    {
        if (volos.SegmentNumber == 0)
            return null;

        return _hair[volos.SegmentNumber - 1, volos.PositionNumber];
    }

    public Volos GetFirstColorisedSegment(Volos volos, Material material)
    {
        for (int i = 0; i < CurrentLenght; i++)
        {
            if (_hair[i, volos.PositionNumber].GetComponent<SkinnedMeshRenderer>().material.color == material.color)
            { 
                return _hair[i, volos.PositionNumber];
            }
        }

        return null;
    }

    private void BindJointBody()
    {
        for (int j = 0; j < _startGrowthPoints.Count; j++)
        {
            _hair[CurrentLenght, j].GetComponent<ConfigurableJoint>().connectedBody = _hair[CurrentLenght - 1, j].VolosEnd.GetComponent<Rigidbody>();
        }
    }
}
