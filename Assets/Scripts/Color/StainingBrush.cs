using UnityEngine;

public class StainingBrush : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private Hairstyle _hairstyle;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hairstyle>())
        {
            _hairstyle.AddNewMaterial(_material);
        }
    }
}