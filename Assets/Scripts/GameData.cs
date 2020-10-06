using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    [SerializeField] Slider _healthSlider = null;
    [SerializeField] Level01Controller _levelController = null;
    public int _topHealth = 100;
    public int _health;

    void Start()
    {
        _healthSlider.maxValue = _topHealth;
        _healthSlider.value = _topHealth;
        _health = _topHealth;
    }

    void Update()
    {
        _healthSlider.value = _health;
        if(_health <= 0)
        {
            _levelController.Death();
        }
    }
}
