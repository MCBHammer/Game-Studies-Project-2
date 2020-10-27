using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int _healthRestored = 20;
    GameObject dataManager;
    GameData _gameData;

    void Start()
    {
        dataManager = GameObject.Find("DataManager");
        _gameData = dataManager.GetComponent<GameData>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _gameData._health < _gameData._topHealth)
        {
            _gameData._health += _healthRestored;
            Destroy(this.gameObject);
        }
    }
}
