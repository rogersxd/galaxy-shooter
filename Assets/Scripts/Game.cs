using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float enemyRate = 2.0f;

    private float nextEnemyTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        nextEnemyTime = Time.time + enemyRate;
    }

    // Update is called once per frame
    void Update()
    {
        checkSpawn();
        checkRespawn();
    }

    private void checkSpawn()
    {
        if (Time.time > nextEnemyTime)
        {
            nextEnemyTime = Time.time + enemyRate;
            Spawn();
        }

    }

    private void Spawn()
    {
        Instantiate(enemyPrefab, new Vector3(Random.Range(-7.73f, 7.73f), 6.60f, 0), Quaternion.identity);
    }

    private void checkRespawn()
    {
        if (transform.position.y < -6.0f)
        {
            Spawn();

            Destroy(this.gameObject);
        }
    }
}
