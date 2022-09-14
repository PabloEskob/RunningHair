using UnityEngine;

[RequireComponent(typeof(BoxCollider),typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    { 
        other.GetComponent<Item>().Destoy();
    }
}
