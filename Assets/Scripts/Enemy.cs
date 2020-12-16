using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2.0f;

    private int healthPoints = 5;

    public GameObject enemyExplosionPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser laser = collision.GetComponent<Laser>();

        if (laser)
        {
            Destroy(laser);
            checkDamage();
        }


        Player player = collision.GetComponent<Player>();

        if (player)
        {
            Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(player);
        }
    }

    private void checkDamage()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        player.score++;

        healthPoints --;

        if (healthPoints <= 0)
        {
            player.score += 5;

            Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }
}
