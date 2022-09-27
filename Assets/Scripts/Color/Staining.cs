using System;
using UnityEngine;

public class Staining : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private Hairstyle _hairstyle;
    [SerializeField] private Material _materialDown;
    [SerializeField] private Material _materialUp;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Hair hair))
        {
           hair.ChangeMaterial(_material);
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Hair hair))
        {
            _hairstyle.FindPreviosHair(hair).ChangeMaterial(_materialDown);
            
            if (_materialUp!=null)
            {
                _hairstyle.FindNextHair(hair).ChangeMaterial(_materialUp);
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.TryGetComponent(out Hair hair))
        {
            hair.SetColorMaterial(_material);
        }
    }*/
}