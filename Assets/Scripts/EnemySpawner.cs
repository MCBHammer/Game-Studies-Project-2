using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject Enemy;
    public Transform[] spawnPoints;

    public bool activeSpawning = false;

    [SerializeField]
    int pauseTime = 5; //may need to customize for your game

    [SerializeField]
    int numToSpawn = 3; //customize as needed

    // Start is called before the first frame update
    void Start()
    {
        activeSpawning = true;
        StartSpawning();
    }

    ///Will be exectued in MiniGameManager when startButton clicked
    //MiniGameState is set to active
    public void StartSpawning()
    {
        Debug.Log("Start Spawning called");
        //Loop generates multiple delayed invocations of SpawnPrefab( ) method.
        for (int i = 0; i < numToSpawn; i++)
        {
            Invoke("SpawnPrefab", pauseTime * i * 2);  //Delays between SpawnPrefab( ) exection
        }
    }

    void SpawnPrefab()
    {
        if (activeSpawning) //won't actually spawn anything unless this is true 
        {
            //Where to spawn based on transform of Spawner gameObject
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            
            Vector3 position = spawnPoints[spawnIndex].position;

            GameObject prefab;
            prefab = Instantiate(Enemy, position, Quaternion.identity);
        }
    }//end SpawnPrefab
}// end class Spawner