using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hairstyle : MonoBehaviour
{
    [SerializeField] private CreatePoints _createPoints;
    [SerializeField] private Head _head;
    [SerializeField] private Hair _hair;
    [SerializeField] private Body _body;
    [SerializeField] private int _maxMass;

    private Queue<Hair> _hairs;
    private List<Hair> _hairsList;

    private void Start()
    {
        _hairsList = new List<Hair>();
        _hairs = new Queue<Hair>();
        PutHairRoots();
    }

    private void PutHairRoots()
    {
        var cratePoint = _createPoints.FindPlaceWithCircle();

        foreach (var position in cratePoint)
        {
            var hair = Instantiate(_hair, position , Quaternion.identity);
            hair.ReduceWeightRigidbody(_maxMass);
            hair.transform.parent = _head.transform;
            hair.Join(GetComponent<Rigidbody>());
            _hairs.Enqueue(hair);
        }

        _maxMass -= 1;
    }

    public void PutHair()
    {
        for (int i = 0; i < _hairs.Count; i++)
        {
            var hairDequeue = _hairs.Dequeue();
            hairDequeue.DestroyHairTip();
            var newHair = Instantiate(_hair, hairDequeue.hairUp.transform.position, _body.transform.localRotation);
            newHair.ReduceWeightRigidbody(_maxMass);
            newHair.Join(hairDequeue.GetComponent<Rigidbody>());
            _hairs.Enqueue(newHair);
            _hairsList.Add(newHair);
        }

        _maxMass -= 1;
    }
}