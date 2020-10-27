using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform spawnCenter;
    [Header("Set in Inspector")]
    public GameObject healthPrefab, shotgunPrefab;

    public bool activeSpawning = false;

    [SerializeField]
    int pauseTime = 5; //may need to customize for your game

    [SerializeField]
    int numToSpawn = 5; //customize as needed

    [SerializeField]
    float chanceToSpawnShotgun = 0.30f; //modify as needed

    [SerializeField]
    float xRangeLeft = -8.0f; //customize as needed

    [SerializeField]
    float xRangeRight = 8.0f; //customize as needed

    [SerializeField]
    float zRangeForward = -8.0f; //customize as needed

    [SerializeField]
    float zRangeBackward = 8.0f; //customize as needed

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

    //Executed from the StartSpawning method
    //Many instances of this method will be invoked 
    void SpawnPrefab()
    {
        if (activeSpawning) //won't actually spawn anything unless this is true 
        {
            //Where to spawn based on transform of Spawner gameObject

            Vector3 position = spawnCenter.position;
            position.x += Random.Range(xRangeLeft, xRangeRight);
            position.z += Random.Range(zRangeForward, zRangeBackward);

            float rand = Random.value;
            GameObject prefab; //temp variable
            if (rand < chanceToSpawnShotgun)
            { //spawn bad prefab
                prefab = Instantiate(shotgunPrefab, position, transform.rotation);
            }
            else  //instantiate good one
            {
                prefab = Instantiate(healthPrefab, position, transform.rotation);
            }
            prefab.transform.SetParent(this.transform); //all spawned objects will be children of the Spawner gameObject
        }
    }//end SpawnPrefab

    //This will be executed from MiniGameManager
    public void StopAllSpawning()
    {
        CancelInvoke("SpawnPrefab");  //cancels all SpawnPrefab( ) methods waiting to be executed
        activeSpawning = false;
    }
}// end class Spawner

