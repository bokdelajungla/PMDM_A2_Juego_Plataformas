using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public Object enemyPrefab;
    private Vector3 spawnPosition, playerPosition;
    private float xDistance, yDistance, xFar, yFar;
    private bool playerIsNear, enemySpawned;
    // Start is called before the first frame update
    void Start()
    {
        xFar = 10f;
        yFar = 3f;
        playerIsNear = false;
        enemySpawned = false;
        spawnPosition = transform.position;
        playerPosition = GameObject.Find("Player").transform.position;

        StartCoroutine("SpawnEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while(true){
            //UPDATE PLAYER POSITION
            playerPosition = GameObject.Find("Player").transform.position;
            //CALCULATE PLAYER DISTANCE WITH SPAWNER
            xDistance = Mathf.Abs(playerPosition.x - spawnPosition.x);
            yDistance = Mathf.Abs(playerPosition.y - spawnPosition.y);

            if(xDistance < xFar && yDistance < yFar){
                playerIsNear = true;
            }
            else
            {
                playerIsNear = false;
            }
            
            if(!playerIsNear && !enemySpawned)
            {
                //CREATED AS CHILD OF SPAWNER
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, this.transform);
                enemySpawned = true;
            }
            
            if(transform.childCount == 0)
            {
                enemySpawned = false;
            }
            //WAIT FOR 2 SECOND BEFORE CHECKING AGAIN
            yield return new WaitForSeconds(2);
        }
        
    }
}