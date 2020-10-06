using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    [SerializeField] GameData _gameData = null;
    public int _damageDealt = 20;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _gameData._health -= _damageDealt;
        }
    }
}
