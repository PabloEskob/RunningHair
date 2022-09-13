using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Dyer : MonoBehaviour
{
    [SerializeField] private Hair _hair;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _dye;
    [SerializeField] private Material _upperGradient;
    [SerializeField] private Material _lowerGradient;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Volos volos))
        {
            volos.Colorise(_dye);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Volos volos) && volos.SegmentNumber == _hair.CurrentLenght)
        {
            Debug.Log(_defaultMaterial);
            if (_hair.GetFirstColorisedSegment(volos, _dye).SegmentNumber > 0)
            {
                _hair.GetLowerSegment(_hair.GetFirstColorisedSegment(volos, _dye)).GetComponent<SkinnedMeshRenderer>().material = _lowerGradient;
            }
        }
    }
}
