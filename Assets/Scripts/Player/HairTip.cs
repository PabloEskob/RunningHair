using UnityEngine;

public class HairTip : MonoBehaviour
{
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetNewMaterial(Material materialColor)
    {
        if (_renderer != null)
        {
            _renderer.material = materialColor;
        }
    }
}