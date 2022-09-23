using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hairstyle : MonoBehaviour
{
    [SerializeField] private CreatePoints _createPoints;
    [SerializeField] private Head _head;
    [SerializeField] private Hair _hair;
    [SerializeField] private int _maxMass;

    private int _squadNumberHair;
    private int _numberHair;
    private Queue<Hair> _hairs;
    private List<Hair> _hairsList;
    private Hair _targetHair;

    private void Start()
    {
        _hairsList = new List<Hair>();
        _hairs = new Queue<Hair>();
        PutHairRoots();
    }

    private Hair FindHair(Hair hair, int value)
    {
        var squareNumber = hair.SetSquadNumber();
        var number = hair.SetNumber();

        foreach (var hairs in _hairsList)
        {
            if (hairs.FindHair(squareNumber + value, number)!=null)
            {
                _targetHair = hairs.FindHair(squareNumber + value, number);
            }
        }
        return _targetHair;
    }

    private void PutHairRoots()
    {
        var cratePoint = _createPoints.FindPlaceWithCircle();

        foreach (var position in cratePoint)
        {
            var hair = Instantiate(_hair, position, _hair.AssighIntialRotation());
            _hairsList.Add(hair);
            hair.ReduceWeightRigidbody(_maxMass);
            hair.transform.parent = _head.transform;
            hair.Join(GetComponent<Rigidbody>());
            _hairs.Enqueue(hair);
        }

        _maxMass -= 1;
    }

    public void PutHair()
    {
        _numberHair = 0;
        _squadNumberHair++;
        for (int i = 0; i < _hairs.Count; i++)
        {
            _numberHair++;
            var hairDequeue = _hairs.Dequeue();
            hairDequeue.DestroyHairTip();
            var newHair = Instantiate(_hair, hairDequeue.HairUp.transform.position, hairDequeue.AssighIntialRotation());
            newHair.Uint(_squadNumberHair, _numberHair);
            hairDequeue.AllowUseForce(false);
            _hairsList.Add(newHair);
            newHair.ReduceWeightRigidbody(_maxMass);
            newHair.Join(hairDequeue.GetComponent<Rigidbody>());
            _hairs.Enqueue(newHair);
        }

        _maxMass -= 1;
    }

    public void AddNewMaterial(Color colorMaterial)
    {
        if (_hairsList != null)
        {
            foreach (var hair in _hairsList)
            {
                hair.SetColorMaterial(colorMaterial);
                hair.HairTip.SetColorMaterial(colorMaterial);
            }
        }
    }
    
    public Hair FindNextHair(Hair hair)
    {
        return FindHair(hair, 1);
    }

    public Hair FindPreviosHair(Hair hair)
    {
        return FindHair(hair, -1);
    }
}