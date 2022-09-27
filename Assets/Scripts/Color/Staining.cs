using UnityEngine;

public class Staining : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private Hairstyle _hairstyle;
    [SerializeField] private Material _materialGradient;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Hair hair))
        {
            var previosHair = _hairstyle.FindPreviosHair(hair);
            previosHair.SetGradient(_materialGradient);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.TryGetComponent(out Hair hair))
        {
            hair.ChangeMaterial(_material);
        }
    }
}