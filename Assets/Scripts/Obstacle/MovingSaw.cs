using UnityEngine;
using DG.Tweening;

public class MovingSaw : MonoBehaviour,IMovable
{
    [SerializeField] private float _duration;
    [SerializeField] private float _direction;

    public void StartMove()
    {
        Move(_direction);
    }

    public void Move(float direction)
    {
        transform.DOLocalMoveX(direction, _duration).SetLoops(-1,LoopType.Yoyo);
    }
}
