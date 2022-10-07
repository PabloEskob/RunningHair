using UnityEngine;

public class HairMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private bool _isMove;

    private void FixedUpdate()
    {
        if (_isMove)
        {
            Move();
        }
    }

    public void StartMove()
    {
        _isMove = true;
    }

    private void Move()
    {
        transform.Rotate(new Vector3(0, _speed, 0) * Time.fixedDeltaTime);
    }
}