using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottleCounter : MonoBehaviour
{
    private TMP_Text _text;

    private int _bottleCount;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();

        _bottleCount = 1;
    }

    private void Update()
    {
        _text.text = $"{_bottleCount}";
    }

    public void AddOne()
    {
        _bottleCount++;
    }
}
