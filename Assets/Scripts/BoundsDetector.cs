using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsDetector : MonoBehaviour
{
    [SerializeField] GameData _gameData = null;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _gameData._health -= _gameData._topHealth;
        }
    }
}
