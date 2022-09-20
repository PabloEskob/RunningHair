using UnityEngine;

[RequireComponent(typeof(BoxCollider),typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private Hairstyle _hairstyle;
    
    private void OnTriggerEnter(Collider other)
    { 
        other.TryGetComponent(out Item item);
        item.Destoy();
        
        if (item.GetComponent<Bottle>())
        {
           _hairstyle.PutHair();
        }
    }
}
