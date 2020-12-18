using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2.0f;

    private int healthPoints = 5;

    public GameObject enemyExplosionPrefab;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
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
            Damage();
        }


        Player player = collision.GetComponent<Player>();

        if (player)
        {
            PullDown();
        }
    }

    private void Damage()
    {
        healthPoints --;

        if (healthPoints <= 0)
        {
            PullDown();
        }
    }

    public void PullDown()
    {
        _uiManager.UpdateScore(5);

        Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }
}
