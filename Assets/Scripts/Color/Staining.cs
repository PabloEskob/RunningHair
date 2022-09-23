using UnityEngine;

public class Staining : MonoBehaviour
{
    [SerializeField] private Hairstyle _hairstyle;
    [SerializeField] private Color _targetMaterial;
    [SerializeField] private Material _materialDown;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Hair hair))
        {
            hair.SetColorMaterial(_targetMaterial);
            _hairstyle.FindPreviosHair(hair).ChangeMaterial(_materialDown);
        }
    }
}