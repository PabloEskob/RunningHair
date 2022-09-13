using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Crystall : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private CrystallCounter _counter;
    [SerializeField] private List<Sprite> _bubbleEffects = new List<Sprite>();
    [SerializeField] private bool _isBlowable;
    [SerializeField] private GameObject _sprite;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _endPositionOnCanvas;


    private SpriteRenderer _spriteRenderer;

    public event Action<Vector2> PickedUp;

    private void Awake()
    {
        if (_isBlowable)
            _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _counter.AddOne();

            ShowCollectEffect();

            DIsableMeshRenderer();

            if (_isBlowable)
            {
                StartCoroutine(Blow());
            }
        }
    }

    private void ShowCollectEffect()
    {
        Vector2 positionOnCanvas = RectTransformUtility.WorldToScreenPoint(_camera.GetComponent<Camera>(), transform.position);
        GameObject crystall = Instantiate(_sprite, positionOnCanvas, Quaternion.identity, _canvas.transform);

        crystall.transform.DOMove(_endPositionOnCanvas.position, 0.5f);
    }

    private void DIsableMeshRenderer()
    {
        MeshRenderer[] views = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer view in views)
            view.enabled = false;
    }

    private IEnumerator Blow()
    {
        float delay = 0.05f;

        foreach (Sprite bubbleEffect in _bubbleEffects)
        {
            transform.LookAt(_camera);
            _spriteRenderer.sprite = bubbleEffect;
            yield return new WaitForSeconds(delay);
        }

        _spriteRenderer.enabled = false;
    }
}
