using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private Hairstyle _hairstyle;
    [SerializeField] private Material _material;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            item.Destroy();

            if (item.GetComponent<Bottle>())
            {
                _hairstyle.PutHair();
                SetGradient();
            }
        }
    }
    
    private void SetGradient()
    {
        var listLastHair = _hairstyle.FindLastHair();

        foreach (var hair in listLastHair)
        {
            var previosHair = _hairstyle.FindPreviosHair(hair);

            if (previosHair != null)
            {
                var rendererHair = hair.GetComponent<Renderer>();
                var rendereHairPrevios = previosHair.GetComponent<Renderer>();
                
                if (rendereHairPrevios.material.color != rendererHair.material.color)
                {
                   previosHair.SetGradient(_material);
                }
            }
        }
    }
}