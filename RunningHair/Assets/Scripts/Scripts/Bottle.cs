using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Bottle : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private BottleCounter _counter;
    [SerializeField] private Hair _hair;
    [SerializeField] private List<Sprite> _blowEffects1 = new List<Sprite>();
    [SerializeField] private List<Sprite> _blowEffects2 = new List<Sprite>();

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _counter.AddOne();

            DIsableMeshRenderer();
            StartCoroutine(Blow());

            _hair.Grow();
        }
    }

    private void DIsableMeshRenderer()
    {
        MeshRenderer[] views = GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer view in views)
            view.enabled = false;
    }

    private IEnumerator Blow()
    {
        float delay = 0.05f;

        foreach (Sprite blowEffect in ChooseRandomEffect())
        {
            transform.LookAt(_camera);
            _spriteRenderer.sprite = blowEffect;
            yield return new WaitForSeconds(delay);
        }

        _spriteRenderer.enabled = false;
    }

    private IReadOnlyList<Sprite> ChooseRandomEffect()
    {
        var random = (min: 0, max: 2);
        int effectNumber = Random.Range(random.min, random.max);

        if (effectNumber == random.min)
            return _blowEffects1;
        else
            return _blowEffects2;
    }
}
