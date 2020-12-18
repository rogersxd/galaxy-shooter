using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Enemies
    public GameObject enemyPrefab;

    //PowerUps
    public GameObject powerUpTripleShoot;
    public GameObject powerUpSuperSpeed;
    public GameObject powerUpSuperShield;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-7.73f, 7.73f), 6.60f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            CreatePowerUp();
            yield return new WaitForSeconds(20.0f);
        }
    }

    private void CreatePowerUp()
    {
        float randomNumber = Random.Range(0.0f, 1.0f);

        if (randomNumber > 0.0f && randomNumber < 0.3f)
        {
            Instantiate(powerUpTripleShoot, new Vector3(Random.Range(-7.73f, 7.73f), 6.60f, 0), Quaternion.identity);
            return;
        }

        if (randomNumber > 0.3f && randomNumber < 0.6f)
        {
            Instantiate(powerUpSuperShield, new Vector3(Random.Range(-7.73f, 7.73f), 6.60f, 0), Quaternion.identity);
            return;
        }

        Instantiate(powerUpSuperSpeed, new Vector3(Random.Range(-7.73f, 7.73f), 6.60f, 0), Quaternion.identity);
    }
}
