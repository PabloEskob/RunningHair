using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string VerticalAxis = "Horizontal";
    private float _movement;

    public float Movement => _movement;

    private void Update()
    {
        _movement = Input.GetAxis(VerticalAxis);
    }
}