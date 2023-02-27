using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class enemySpawner : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject enemy;
    //we need time between each spawning
    float timeBetweenSpawns;
    //how often to spawn the enemey
    public float maxSpawnInt;
 

    //i need to call the sapwning point (transform.position) as a oisition for my enemy spanwers 
    public Transform[] spawningPoints;
    public int maxEnimies;
    //specifiy the number of enemies
    public int numOfEnemies;
    void Start()
    {
        numOfEnemies = 0;
        timeBetweenSpawns = maxSpawnInt;
    }
    void Update()
    {
        if ((PhotonNetwork.IsMasterClient) && (PhotonNetwork.CurrentRoom.PlayerCount > 1))
        {

            if (timeBetweenSpawns <= 0.0f)
            {
                if (numOfEnemies <= maxEnimies)
                { 
                    int indexRand = Random.Range(0, spawningPoints.Length);
                    //the position of the random spawners
                    Vector3 randSpawnPos = spawningPoints[indexRand].position;
                    PhotonNetwork.Instantiate(enemy.name, randSpawnPos, Quaternion.identity);
                    numOfEnemies = numOfEnemies + 1;
                    timeBetweenSpawns = maxSpawnInt;
                }
            }
            else
            {
                timeBetweenSpawns -= Time.deltaTime;
            }
        }
    }
}
