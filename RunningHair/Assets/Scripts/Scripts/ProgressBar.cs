using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider _slider;
    private float _startValue;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _startValue = 0.15f;
    }

    private void Start()
    {
        _slider.value = _startValue;
        StartCoroutine(Fill());
    }

    private IEnumerator Fill()
    {
        float duration = 21.5f;
        float timeLeft = default;
        float normalized = default;

        while (normalized < 1)
        {
            timeLeft += Time.deltaTime;
            normalized = timeLeft / duration; 
            _slider.value = Mathf.Lerp(_startValue, _slider.maxValue, normalized);
            yield return null;
        }
    }
}
