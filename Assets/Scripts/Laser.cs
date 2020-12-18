using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckDestroy();
    }

    void Movement()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void CheckDestroy()
    {
        if (transform.position.y > 6)
        {
            Destroy(this.gameObject);
        }
    }

    //EVENTS
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PowerUpTripleShoot powerUpTripleShoot = GetComponent<PowerUpTripleShoot>();
        PowerUpSuperSpeed powerUpSuperSpeed = GetComponent<PowerUpSuperSpeed>();
        PowerUpSuperShield powerUpSuperShield = GetComponent<PowerUpSuperShield>();

        if (powerUpTripleShoot
            || powerUpSuperSpeed
            || powerUpSuperShield)
        {
            return;
        }

        Destroy(this.gameObject);
    }
}
