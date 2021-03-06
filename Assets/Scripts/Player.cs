﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject shieldGameObject;
    public GameObject playerExplosionPrefab;
    public GameObject thrusterPrefab;

    private UIManager _uiManager;
    private AudioSource _laserAudio;

    public float speed = 5.0f;
    public float horizontalInput;
    public float verticalInput;
    public bool canTripleShoot = false;
    public bool shield = false;

    private float _fireRate = 0.10f;
    private float _nextFireTime = 0.0f;
    private int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START GAME");

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _uiManager.UpdateLives(lives);

        _laserAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Collide();
        Movement();
        Fire();
    }

    private void Collide()
    {
        if (transform.position.x > 8.24f)
        {
            transform.position = new Vector3(8.24f, transform.position.y, 0);
        }

        if (transform.position.x < -8.34f)
        {
            transform.position = new Vector3(-8.34f, transform.position.y, 0);
        }


        if (transform.position.y > 4.29f)
        {
            transform.position = new Vector3(transform.position.x, 4.29f, 0);
        }

        if (transform.position.y < -4.27f)
        {
            transform.position = new Vector3(transform.position.x, -4.27f, 0);
        }
    }

    private void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);

        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

        if (verticalInput > 0.0f)
        {
            thrusterPrefab.SetActive(true);
            return;
        }

        thrusterPrefab.SetActive(false);
    }

    private void Fire()
    {
        if (canTripleShoot 
            && Input.GetMouseButton(0)
            && Time.time > _nextFireTime)
        {
            Vector3 firstShoot = transform.position;
            firstShoot.x -= 0.5f;

            Vector3 thirdShoot = transform.position;
            thirdShoot.x += 0.5f;

            Instantiate(laserPrefab, firstShoot, Quaternion.identity);
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
            Instantiate(laserPrefab, thirdShoot, Quaternion.identity);

            _nextFireTime = Time.time + _fireRate;

            _laserAudio.Play();

            return;
        }

        if (Input.GetMouseButton(0) && Time.time > _nextFireTime)
        {
            Instantiate(laserPrefab, transform.position, Quaternion.identity);

            _nextFireTime = Time.time + _fireRate;

            _laserAudio.Play();
        }
    }

    public void TripleShootOn()
    {
        canTripleShoot = true;
        StartCoroutine(PowerUpCoolDown());
    }

    public void SuperSpeedOn()
    {
        speed = 20.0f;
        StartCoroutine(PowerUpCoolDown());
    }

    public void SuperShieldOn()
    {
        shield = true;

        shieldGameObject.SetActive(true);

        StartCoroutine(PowerUpCoolDown());
    }

    public IEnumerator PowerUpCoolDown()
    {
        yield return new WaitForSeconds(30.0f);

        canTripleShoot = false;
        shield = false;
        speed = 5.0f;

        shieldGameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (shield)
        {
            return;
        }

        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy)
        {
            Damage();
        }
    }

    void Damage()
    {
        lives--;

        if (lives < 0)
        {
            PullDown();
        }

        _uiManager.UpdateLives(lives);
    }

    public void PullDown()
    {
        Instantiate(playerExplosionPrefab, transform.position, Quaternion.identity);

        transform.position = new Vector3(0.02f, -4.09f, 0);
    }
}
