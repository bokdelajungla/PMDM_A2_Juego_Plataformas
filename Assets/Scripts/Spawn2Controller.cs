using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn2Controller : MonoBehaviour
{
    public GameObject enemyPrefab;
    private Vector3 randomPosition;
    private float xMin = 5f;
    private float xMax = 150f;
    private float yMin = -9f;
    private float yMax = 9.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy() {
        yield return new WaitForSeconds(2f);
        while(true) {
            randomPosition = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(18f, 20f));
        }
    }
}
