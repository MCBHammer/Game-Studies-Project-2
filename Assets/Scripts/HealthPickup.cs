using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] GameData _gameData = null;
    public int _healthRestored = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _gameData._health < _gameData._topHealth)
        {
            _gameData._health += _healthRestored;
            Destroy(this.gameObject);
        }
    }
}
