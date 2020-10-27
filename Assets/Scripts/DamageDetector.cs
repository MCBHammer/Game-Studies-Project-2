using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    GameObject dataManager;
    GameData _gameData;
    public int _damageDealt = 20;

    void Start()
    {
        dataManager = GameObject.Find("DataManager");
        _gameData = dataManager.GetComponent<GameData>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _gameData._health -= _damageDealt;
        }
    }
}
