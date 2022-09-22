using UnityEngine;

public class Staining : MonoBehaviour
{
    [SerializeField] private Material _material;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Hair hair))
        {
            hair.SetColorMaterial(_material);
        }
    }
}