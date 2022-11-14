using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hairstyle : MonoBehaviour
{
    [SerializeField] private CreatePoints _createPoints;
    [SerializeField] private HairMovement _head;
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
        var squareNumber = hair.SquadNumber;
        var number = hair.Number;

        foreach (var hairs in _hairsList)
        {
            if (hairs.FindHair(squareNumber + value, number) != null)
            {
                _targetHair = hairs.FindHair(squareNumber + value, number);
            }
        }

        return _targetHair;
    }

    private void PutHairRoots()
    {
        var createPoint = _createPoints.FindPlaceWithCircle();

        foreach (var position in createPoint)
        {
            var hair = Instantiate(_hair, position, _hair.AssighIntialRotation(), _head.transform);
            _hairsList.Add(hair);
            hair.ReduceWeightRigidbody(_maxMass);
            hair.Join(_head.GetComponent<Rigidbody>());
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
            CreateNewHair(hairDequeue);
        }

        _maxMass -= 1;
    }

    public void AddNewMaterial(Material material)
    {
        if (_hairsList != null)
        {
            foreach (var hair in _hairsList)
            {
                hair.ChangeMaterial(material);
                hair.TargetHairTip.SetNewMaterial(material);
            }
        }
    }

    public Hair FindPreviosHair(Hair hair)
    {
        return FindHair(hair, -1);
    }

    public void SetGradient(Material materialGreen)
    {
        var listLastHair = FindLastHair();

        foreach (var hair in listLastHair)
        {
            var previosHair = FindPreviosHair(hair);

            if (previosHair != null)
            {
                var rendererHair = hair.GetComponent<Renderer>();
                var rendereHairPrevios = previosHair.GetComponent<Renderer>();

                if (rendereHairPrevios.material.color != rendererHair.material.color)
                {
                    if (rendereHairPrevios.material.color == materialGreen.color)
                    {
                        rendererHair.enabled = false;
                        hair.OnEnableMaterial(0);
                    }
                    else
                    {
                        rendererHair.enabled = false;
                        hair.OnEnableMaterial(2);
                    }
                }
            }
        }
    }

    public void Finish(float forse)
    {
        foreach (var hair in _hairsList)
        {
            hair.FinishGivingForse(forse);
            hair.DisableCharacterJoint();
            hair.DisableDrag();
        }
    }

    public List<Hair> FindLastHair()
    {
        List<Hair> lastHair = new List<Hair>();

        foreach (var hair in _hairsList)
        {
            if (hair.SquadNumber == _squadNumberHair)
            {
                lastHair.Add(hair);
            }
        }

        return lastHair;
    }

    private void CreateNewHair(Hair hairDequeue)
    {
        var newHair = Instantiate(_hair, hairDequeue.HairUp.transform.position, hairDequeue.AssighIntialRotation(),_head.transform);
        newHair.Uint(_squadNumberHair, _numberHair);
        newHair.ReduceWeightRigidbody(_maxMass);
        newHair.Join(hairDequeue.GetComponent<Rigidbody>());
        _hairsList.Add(newHair);
        _hairs.Enqueue(newHair);
    }
}