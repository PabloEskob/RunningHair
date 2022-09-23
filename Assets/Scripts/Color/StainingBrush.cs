using UnityEngine;

public class StainingBrush : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private Color _materialColor;
    [SerializeField] private Hairstyle _hairstyle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Hairstyle>())
        {
            _hairstyle.AddNewMaterial(_material,_materialColor);
        }
    }
}