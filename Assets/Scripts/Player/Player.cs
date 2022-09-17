using UnityEngine;

[RequireComponent(typeof(BoxCollider),typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    { 
        other.TryGetComponent(out Item item);
        item.Destoy();
    }
}
