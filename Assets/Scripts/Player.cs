using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;

    public float horizontalInput;
    public float verticalInput;

    public GameObject laserPrefab;
    public GameObject powerUpTripleShoot;
    public GameObject powerUpSuperSpeed;

    public GameObject playerExplosionPrefab;

    public bool canTripleShoot = false;

    public float fireRate = 0.25f;
    private float nextFireTime = 0.0f;

    public int score = 0;
    public int scoreRatePowerUp = 50;
    private int nextScorePowerUp = 0;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START GAME");
        
        nextScorePowerUp += scoreRatePowerUp;
    }

    // Update is called once per frame
    void Update()
    {
        Collide();
        Movement();
        Fire();
        Score();
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
    }

    private void Fire()
    {
        if (canTripleShoot 
            && Input.GetMouseButton(0)
            && Time.time > nextFireTime)
        {
            Vector3 firstShoot = transform.position;
            firstShoot.x -= 0.5f;

            Vector3 thirdShoot = transform.position;
            thirdShoot.x += 0.5f;

            Instantiate(laserPrefab, firstShoot, Quaternion.identity);
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
            Instantiate(laserPrefab, thirdShoot, Quaternion.identity);

            nextFireTime = Time.time + fireRate;

            return;
        }

        if (Input.GetMouseButton(0) && Time.time > nextFireTime)
        {
            Instantiate(laserPrefab, transform.position, Quaternion.identity);

            nextFireTime = Time.time + fireRate;
        }
    }

    private void Score()
    {
        if (score >= nextScorePowerUp)
        {
            CreatePowerUp();
            nextScorePowerUp = score + scoreRatePowerUp;
        }
    }

    private void CreatePowerUp()
    {
        if (Random.Range(0.0f, 1.0f) > 0.5)
        {
            Instantiate(powerUpTripleShoot, new Vector3(Random.Range(-7.73f, 7.73f), 6.60f, 0), Quaternion.identity);
        }

        Instantiate(powerUpSuperSpeed, new Vector3(Random.Range(-7.73f, 7.73f), 6.60f, 0), Quaternion.identity);
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

    public IEnumerator PowerUpCoolDown()
    {
        yield return new WaitForSeconds(5.0f);

        canTripleShoot = false;
        speed = 5.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy)
        {
            Instantiate(playerExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
