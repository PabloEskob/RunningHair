using UnityEngine;

public class Staining : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private Hairstyle _hairstyle;
    [SerializeField] private int _number;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Hair hair))
        {
            hair.ChangeMaterial(_material);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.TryGetComponent(out Hair hair))
        {
            SetColorDownHair(hair);
        }
    }

    private void SetColorDownHair(Hair hair)
    {
        var previosHair = _hairstyle.FindPreviosHair(hair);
        var rendererHair = previosHair.GetComponent<Renderer>();

        if (previosHair.Renderer.material.color != _material.color)
        {
            rendererHair.enabled = false;
            previosHair.OnEnableMaterial(_number);
        }
    }
}