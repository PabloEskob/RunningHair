using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private Hairstyle _hairstyle;
    [SerializeField] private Material _materialGreen;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            item.Destroy();

            if (item.GetComponent<Bottle>())
            {
                _hairstyle.PutHair();
                _hairstyle.SetGradient(_materialGreen);
            }
        }
    }
}