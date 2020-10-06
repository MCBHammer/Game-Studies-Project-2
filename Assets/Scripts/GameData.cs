using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    [SerializeField] Slider _healthSlider = null;
    public int _topHealth = 100;
    int _health;

    void Start()
    {
        _healthSlider.maxValue = _topHealth;
        _healthSlider.value = _topHealth;
        _health = _topHealth;
    }

    void Update()
    {
        _healthSlider.value = _health;
    }
}
