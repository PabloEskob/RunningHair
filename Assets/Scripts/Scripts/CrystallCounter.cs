using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrystallCounter : MonoBehaviour
{
    private TMP_Text _text;

    private int _crystallCount;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();

        _crystallCount = 58;
    }

    private void Update()
    {
        _text.text = $"{_crystallCount}";    
    }

    public void AddOne()
    {
        _crystallCount++;
    }
}
